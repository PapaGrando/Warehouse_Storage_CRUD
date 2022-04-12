using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WarehouseCRUD.Storage.DataContext;
using WarehouseCRUD.Storage.Models;

namespace WarehouseCRUD.Storage.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WarehouseCRUD.Storage.DataContext.StorageDbContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public IndexModel(WarehouseCRUD.Storage.DataContext.StorageDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }
        public string DefaultImage => Path.Combine(_appEnvironment.WebRootPath, "DefaultProd.png");

        public async Task OnGetAsync()
        {
            Product = await _context.Products.ToListAsync();
        }
    }
}
