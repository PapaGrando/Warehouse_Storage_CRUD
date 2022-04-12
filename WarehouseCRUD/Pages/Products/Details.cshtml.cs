using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models;

namespace WarehouseCRUD.Storage.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;

        public DetailsModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context)
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

            Product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
