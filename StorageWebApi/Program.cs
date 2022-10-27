using Microsoft.EntityFrameworkCore;
using Storage.DataBase.DataContext;
using System.Reflection;
using WarehouseCRUD.Storage.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StorageDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("PgConnection")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddLogging();

builder.Services.AddRepoServices();
builder.Services.AddStorageServises();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
