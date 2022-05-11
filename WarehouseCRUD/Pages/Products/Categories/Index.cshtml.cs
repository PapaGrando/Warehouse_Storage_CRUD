using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models;
using WarehouseCRUD.Storage.Models.Helpers;
using WarehouseCRUD.Storage.Sevices;

namespace WarehouseCRUD.Storage.Pages.Products.Categories
{
    public class IndexModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;
        private readonly IRazorRenderService _renderService;
        public IList<ProductCategory> ProductCategories { get; set; }

        public IndexModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context,
            IRazorRenderService renderService)
        {
            _context = context;
            _renderService = renderService;
        }

        public async Task OnGetAsync()
        {

        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            ProductCategories = await _context.ProductCategories.ToListAsync();
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
                var thisCat = await _context.ProductCategories.FindAsync(id);
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
                    await _context.ProductCategories.AddAsync(cat);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Entry(cat).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                bool ProductCategoryExists(int id)
                {
                    return _context.ProductCategories.Any(e => e.Id == id);
                }

                ProductCategories = await _context.ProductCategories.ToListAsync();
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
            var cat = await _context.ProductCategories.FindAsync(id);

            if (cat == null)
                return new JsonResult(new { isValid = false});

            if (_context.Entry(cat).Collection(x => x.Products).Query().Any())
                return new JsonResult(new { isValid = false });

            _context.ProductCategories.Remove(cat);
            await _context.SaveChangesAsync();

            ProductCategories = await _context.ProductCategories.ToListAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", ProductCategories);
            return new JsonResult(new { isValid = true, html = html});
        }
    }
}
