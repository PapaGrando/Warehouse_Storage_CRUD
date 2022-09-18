using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Storage.Core.DTO;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using WarehouseCRUD.Storage.Models;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Pages.Storage.Areas
{
    public class IndexModel : PageModel
    {
        private readonly IRazorRenderService _renderService;
        private readonly IAreaRepoAsync _areaRepo;
        private readonly IUnitOfWorkAsync _uw;

        public IndexModel(IAreaRepoAsync areaRepo, IUnitOfWorkAsync uw, IRazorRenderService renderService)
        {
            _areaRepo = areaRepo;
            _uw = uw;
            _renderService = renderService;
        }

        public IList<AreaInfo> AreasInfo { get;set; }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            AreasInfo = await _areaRepo.GetAreasInfoAsync();

            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<AreaInfo>>
                (ViewData, AreasInfo)
            };
        }

        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var cat = await _areaRepo.GetByIdAsync(id);

            if (cat == null)
                return new JsonResult(new { isValid = false });

            //Проверяем наличие товаров в зоне
            if (await _areaRepo.IsContainsStorageItemsInArea(id))
                return new JsonResult(new { isValid = false });

            await _areaRepo.DeleteAsync(cat);
            await _uw.Commit();

            AreasInfo = await _areaRepo.GetAreasInfoAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", AreasInfo);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
