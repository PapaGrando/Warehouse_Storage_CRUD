using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Storage.Core.Models.Storage;
using Storage.DataBase.DataContext;

namespace WarehouseCRUD.Storage.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly StorageDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public IndexModel(StorageDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
