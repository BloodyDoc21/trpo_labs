using laba1.Repositories;
using MvcLab1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddSingleton<IWorkoutRepository, InMemoryWorkoutRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "about",
    pattern: "about-us",
    defaults: new { controller = "Home", action = "Privacy" });

app.MapControllerRoute(
    name: "userProfile",
    pattern: "user/{username}/{action=Profile}",
    defaults: new { controller = "Demo" });

app.MapControllerRoute(
    name: "product",
    pattern: "product/{id:int}",
    defaults: new { controller = "Demo", action = "ProductDetails" });

app.Run();
