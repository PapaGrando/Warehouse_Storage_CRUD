using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace WarehouseCRUD.Storage.Pages.Storage.Areas
{
    public class DeleteModel : PageModel
    {
        private readonly StorageDbContext _context;

        public DeleteModel(StorageDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Area Area { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Area = await _context.Areas.FirstOrDefaultAsync(m => m.Id == id);

            if (Area == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Area = await _context.Areas.FindAsync(id);

            if (Area != null)
            {
                _context.Areas.Remove(Area);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
