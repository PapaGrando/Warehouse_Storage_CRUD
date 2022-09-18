using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Storage.Core.DTO;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using Storage.DataBase.Repos;
using WarehouseCRUD.Storage.Helpers;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Pages.Storage.Areas
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Area Area { get; set; }

        private readonly IUnitOfWorkAsync _uw;
        private readonly IAreaRepoAsync _areaRepo;
        private readonly IRazorRenderService _renderService;
        private readonly ISubAreaFactory _subAreaFactory;
        private readonly ICellTypeRepoAsync _cellTypeRepo;

        public CreateModel(IUnitOfWorkAsync uw, IAreaRepoAsync areaRepo,
            ISubAreaFactory subAreaFactory, IRazorRenderService renderService,
            ICellTypeRepoAsync cellTypeRepo)
        {
            _uw = uw;
            _areaRepo = areaRepo;
            _subAreaFactory = subAreaFactory;
            _renderService = renderService;
            _cellTypeRepo = cellTypeRepo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostSubmitAreaAsync()
        {
            //Тут необходимо дополнительное уведомление в случае ошибки
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _areaRepo.AddAsync(Area);
            await _uw.Commit();

            return RedirectToPage("./Index");
        }

        public async Task<JsonResult> OnGetCreateSubAreaAsync()
        {
            ViewData["CellTypeId"] = new SelectList(await _cellTypeRepo.GetAllAsync(), "Id", "Name");
            return new JsonResult(new
            {
                isValid = true,
                html =
                 await _renderService.ToStringAsync("_CreateSubArea", new SubAreaConfiguration())
            });
        }

        public async Task<JsonResult> OnPostCreateSubAreaAsync(SubAreaConfiguration sac)
        {
            if (!ValidateSubAreaConfig(sac))
                return await ErrorResult();

            var newSubArea = _subAreaFactory.Create(sac);

            //Тут необходимо дополнительное уведомление в случае ошибки
            if (!ValidateSubArea(newSubArea))
                return await ErrorResult();

            Area.SubAreas.Add(newSubArea);

            var html = await _renderService.ToStringAsync("_ViewAll", Area);
            return new JsonResult(new { isValid = true, html = html });

            async Task<JsonResult> ErrorResult()
            {
                var html = await _renderService.ToStringAsync("_CreateSubArea", sac);
                return new JsonResult(new
                {
                    isValid = false,
                    html = html
                });
            }
        }
        public async Task<JsonResult> OnPostDeleteSubAreaAsync(int noOfSubArea)
        {
            if (noOfSubArea < 0 ||
                noOfSubArea >= Area.SubAreas.Count())
                return new JsonResult(new { isValid = false });

            Area.SubAreas.RemoveAt(noOfSubArea);
            AreaHelper.SetSubAreaNoByListPos(Area.SubAreas);

            var html = await _renderService.ToStringAsync("_ViewAll", Area);
            return new JsonResult(new { isValid = true, html = html });
        }

        #region Validators

        private bool ValidateSubAreaConfig(SubAreaConfiguration sac) =>
            Validator.TryValidateObject(
                sac,
                new ValidationContext(sac),
                new List<ValidationResult>(),
                true);

        private bool ValidateSubArea(SubArea sa) =>
            Validator.TryValidateObject(
                sa,
                new ValidationContext(sa),
                new List<ValidationResult>(),
                true);

        #endregion
    }
}
