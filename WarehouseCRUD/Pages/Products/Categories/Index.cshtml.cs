using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Interfaces;
using Storage.Core.Models;
using Storage.DataBase.DataContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseCRUD.Storage.Sevices;
using WarehouseCRUD.Storage.Sevices.Interfaces;

namespace WarehouseCRUD.Storage.Pages.Products.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryRepoAsync _catRepo;
        private readonly IUnitOfWorkAsync _uw;
        private readonly IRazorRenderService _renderService;
        public IList<ProductCategory> ProductCategories { get; set; }

        public IndexModel(IProductCategoryRepoAsync catRepo, IUnitOfWorkAsync uw, IRazorRenderService renderService)
        {
            _catRepo = catRepo;
            _uw = uw;
            _renderService = renderService;
        }

        public async Task OnGetAsync()
        {

        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            ProductCategories = await _catRepo.GetAllAsync();
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
                return new JsonResult(new
                {
                    isValid = true,
                    html =
                    await _renderService.ToStringAsync("_CreateOrEdit", new ProductCategory())
                });
            else
            {
                var thisCat = await _catRepo.GetByIdAsync(id);
                return new JsonResult(new
                {
                    isValid = true,
                    html = await _renderService.ToStringAsync("_CreateOrEdit", thisCat)
                });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ProductCategory cat)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _catRepo.AddAsync(cat);
                    await _uw.Commit();
                }
                else
                {
                    await _catRepo.UpdateAsync(cat);
                    await _uw.Commit();
                }

                ProductCategories = await _catRepo.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAll", ProductCategories);
                return new JsonResult(new { isValid = true, html = html });
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
            var cat = await _catRepo.GetByIdAsync(id);

            if (cat == null)
                return new JsonResult(new { isValid = false});

            if (await _catRepo.IsContainsProductInCategoryAsync(id))
                return new JsonResult(new { isValid = false });

            await _catRepo.DeleteAsync(cat);
            await _uw.Commit();

            ProductCategories = await _catRepo.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", ProductCategories);
            return new JsonResult(new { isValid = true, html = html});
        }
    }
}
