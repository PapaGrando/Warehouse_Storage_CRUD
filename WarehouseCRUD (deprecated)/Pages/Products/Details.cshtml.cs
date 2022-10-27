using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace WarehouseCRUD.Storage.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly StorageDbContext _context;

        public DetailsModel(StorageDbContext context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products.Include(p => p.ProductCategory).
                FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
