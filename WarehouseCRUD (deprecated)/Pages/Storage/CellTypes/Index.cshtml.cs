using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using Storage.DataBase.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseCRUD.Storage.Helpers;
using WarehouseCRUD.Storage.Sevices;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Pages.Storage.CellTypes
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWorkAsync _uw;
        private readonly IRazorRenderService _renderService;
        private ICellTypeRepoAsync _ctRepo => _uw.CellTypes;
        public IList<CellType> CellTypes { get; set; }

        public IndexModel(IUnitOfWorkAsync uw, 
            IRazorRenderService renderService)
        {
            _uw = uw;
            _renderService = renderService;
        }

        public async Task OnGetAsync() { }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            CellTypes = await _ctRepo.GetAllAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<CellType>>
                (ViewData, CellTypes)
            };
        }
        public async Task<JsonResult> OnGetCreateAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new
                {
                    isValid = true,
                    html =
                    await _renderService.ToStringAsync("_Create", new CellType())
                });
            else
            {
                var thisCat = await _ctRepo.GetByIdAsync(id);
                return new JsonResult(new
                {
                    isValid = true,
                    html = await _renderService.ToStringAsync("_Create", thisCat)
                });
            }
        }
        public async Task<JsonResult> OnPostCreateAsync(int id, CellType ct)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _ctRepo.AddAsync(ct);
                    await _uw.Commit();
                }
                else
                {
                    await _ctRepo.UpdateAsync(ct);
                    await _uw.Commit();
                }

                CellTypes = await _ctRepo.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", CellTypes);
                return new JsonResult(new JqueryRequest { IsValid = true, Html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_Create", ct);
                return new JsonResult(new
                {
                    isValid = false,
                    html
                });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var ct = await _ctRepo.GetByIdAsync(id);

            if (ct == null)
                return new JsonResult(new JqueryRequest
                { IsValid = false, Msg = "Этого типа ячейки не существует", Err = $"{id} CellType not found" });

            try
            {
                await _ctRepo.DeleteAsync(ct);
                await _uw.Commit();
            }
            catch (NoCascadeDeletionException<CellType> ex)
            {
                return new JsonResult(new JqueryRequest
                {
                    IsValid = false,
                    Msg = "В этой категирии еще находятся ячейки",
                });
            }

            CellTypes = await _ctRepo.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", CellTypes);
            return new JsonResult(new JqueryRequest { IsValid = true, Html = html });
        }
    }
}
