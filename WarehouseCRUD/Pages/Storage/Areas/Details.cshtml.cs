using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models.Storage;

namespace WarehouseCRUD.Storage.Pages.Storage.Areas
{
    public class DetailsModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;

        public DetailsModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context)
        {
            _context = context;
        }

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
    }
}
