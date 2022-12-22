using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApplicationPhoto.Web.UI.Models;

namespace ApplicationPhoto.Web.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationPhoto.Web.UI.Models.Photo> Photos { get; set; }
        public DbSet<ApplicationPhoto.Web.UI.Models.Categorie> Categories { get; set; }
        public DbSet<ApplicationPhoto.Web.UI.Models.Voyage> Voyage { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
      

    }
}