using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Interfaces;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;
using Storage.DataBase.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseCRUD.Storage.Helpers;
using WarehouseCRUD.Storage.Sevices;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Pages.Products.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWorkAsync _uw;
        private readonly IRazorRenderService _renderService;
        public IList<ProductCategory> ProductCategories { get; set; }

        public IndexModel(IUnitOfWorkAsync uw, IRazorRenderService renderService)
        {
            _uw = uw;
            _renderService = renderService;
        }

        public async Task OnGetAsync()
        {

        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            ProductCategories = await _uw.ProductCategories.GetAllAsync();
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<ProductCategory>>
                (ViewData, ProductCategories)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new JqueryRequest
                {
                    IsValid = true,
                    Html =
                    await _renderService.ToStringAsync("_CreateOrEdit", new ProductCategory())
                });
            else
            {
                var thisCat = await _uw.ProductCategories.GetByIdAsync(id);
                return new JsonResult(new JqueryRequest
                {
                    IsValid = true,
                    Html = await _renderService.ToStringAsync("_CreateOrEdit", thisCat)
                });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ProductCategory cat)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _uw.ProductCategories.AddAsync(cat);
                    await _uw.Commit();
                }
                else
                {
                    await _uw.ProductCategories.UpdateAsync(cat);
                    await _uw.Commit();
                }

                ProductCategories = await _uw.ProductCategories.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", ProductCategories);
                return new JsonResult(new JqueryRequest { IsValid = true, Html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEdit", cat);
                return new JsonResult(new
                {
                    isValid = false,
                    html = html
                });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var cat = await _uw.ProductCategories.GetByIdAsync(id);

            if (cat == null)
                return new JsonResult(new JqueryRequest
                { IsValid = false, Msg = "Категирии не существует", Err = $"{id} category not found" });

            try
            {
                await _uw.ProductCategories.DeleteAsync(cat);
                await _uw.Commit();
            }
            catch (NoCascadeDeletionException<ProductCategory> ex)
            {
                return new JsonResult(new JqueryRequest
                {
                    IsValid = false,
                    Msg = "В категории еще есть товары",
                });
            }

            ProductCategories = await _uw.ProductCategories.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", ProductCategories);
            return new JsonResult(new JqueryRequest { IsValid = true, Html = html });
        }
    }
}
