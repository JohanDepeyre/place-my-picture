
using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services;
using ApplicationPhoto.Web.UI.Services.Interfaces;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{

    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential 
    // cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;

    options.MinimumSameSitePolicy = SameSiteMode.None;
});
builder.Services.AddTransient<IRepository<Voyage>, VoyageRepository>();
builder.Services.AddTransient<IRepository<Categorie>, CategorieRepository>();
builder.Services.AddTransient<IRepository<Photo>, PhotoRepository>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCookiePolicy();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints (endpoints =>
{
    #region Route Voyage
    endpoints.MapControllerRoute(
      name: "ajoutervoyage",
      pattern: "ajouter-voyage",
      defaults: new { controller = "Voyage", action = "Create" }
      );
        endpoints.MapControllerRoute(
      name: "mesvoyages",
      pattern: "mes-voyages",
      defaults: new { controller = "Voyage", action = "Index" }
      );
    endpoints.MapControllerRoute(
    name: "detailsvoyage",
    pattern: "details-voyage/{id}",
    defaults: new { controller = "Voyage", action = "Details" }
    );
        endpoints.MapControllerRoute(
    name: "editervoyage",
    pattern: "editer-voyage/{id}",
    defaults: new { controller = "Voyage", action = "Edit" }
    );
        endpoints.MapControllerRoute(
    name: "supprimervoyage",
    pattern: "supprimer-voyage/{id}",
    defaults: new { controller = "Voyage", action = "Delete" }
    );
    #endregion
    #region route Creation Image

    endpoints.MapControllerRoute(
     name: "ajouterphoto",
     pattern: "ajouter-photo",
     defaults: new { controller = "Photo", action = "Create" }
     );

    #endregion

    #region route carte

    endpoints.MapControllerRoute(
     name: "votrecarte",
     pattern: "ma-carte",
     defaults: new { controller = "Carte", action = "Index" }
     );
    #endregion
    endpoints.MapControllerRoute(
        name: "showimage",
        pattern: "image-voyage/{id}",
        defaults: new { controller = "Photo", action = "Index" }
 
        );

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.MapRazorPages();

app.Run();
