using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCAlunos.Data;
using MVCAlunos.Models;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
/*builder.Services.AddDbContext<MVCAlunosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MVCAlunosContext") ?? throw new InvalidOperationException("Connection string 'MVCAlunosContext' not found.")));
*/
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
var app = builder.Build();
CultureInfo.CurrentCulture = new CultureInfo("pt-BR", false);
Thread.CurrentThread.CurrentCulture = new CultureInfo("pt");


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
