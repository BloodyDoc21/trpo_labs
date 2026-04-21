using laba1.Repositories;
using Microsoft.EntityFrameworkCore;
using laba1.Data;

var builder = WebApplication.CreateBuilder(args);

// ===== ĞÅÃÈÑÒĞÀÖÈß ÑÅĞÂÈÑÎÂ =====
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLiteConnection")));

// Èñïîëüçóåì EF ğåïîçèòîğèé (InMemory óáèğàåì)
builder.Services.AddScoped<IProductRepository, EfProductRepository>();

builder.Services.AddScoped<IWorkoutRepository, EfWorkoutRepository>();

// ===== ÑÁÎĞÊÀ =====
var app = builder.Build();

// ===== ÈÍÈÖÈÀËÈÇÀÖÈß ÁÄ =====
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await SeedData.InitializeAsync(dbContext);
}

// ===== MIDDLEWARE =====
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