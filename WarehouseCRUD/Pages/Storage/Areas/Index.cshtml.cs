using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models.Storage;

namespace WarehouseCRUD.Storage.Pages.Storage.Areas
{
    public class IndexModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;

        public IndexModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context)
        {
            _context = context;
        }

        public IList<Area> Areas { get;set; }

        public async Task OnGetAsync()
        {
        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Areas = await _context.Areas.ToListAsync();

            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<Area>>
                (ViewData, Areas)
            };
        }
    }
}
