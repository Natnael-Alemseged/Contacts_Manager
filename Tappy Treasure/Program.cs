using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tappy_Treasure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



string? connectionString = Environment.GetEnvironmentVariable("connectionString_Tappy_Treasure");

System.Diagnostics.Debug.WriteLine(connectionString!=null);


if (connectionString!=null)
{
    System.Diagnostics.Debug.WriteLine(connectionString);

	builder.Services.AddDbContext<myDBContext>(options => options.UseSqlServer(connectionString));

}






var app = builder.Build();

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
