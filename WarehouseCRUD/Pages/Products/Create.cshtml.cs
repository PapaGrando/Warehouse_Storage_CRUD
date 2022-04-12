using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models;

namespace WarehouseCRUD.Storage.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;

        public CreateModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
