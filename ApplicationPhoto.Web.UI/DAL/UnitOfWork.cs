using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using Microsoft.EntityFrameworkCore;


namespace ApplicationPhoto.Web.UI.DAL
{
    public class UnitOfWork : IDisposable
    {

        private ApplicationDbContext context ;
        private GenericRepository<Voyage> voyageRepository;
        private GenericRepository<Photo> photoRepository;
        private GenericRepository<Categorie> categorieRepository;



        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
           
        }
        public GenericRepository<Voyage> VoyageRepository
        {
            get
            {

                if (this.voyageRepository == null)
                {
                    this.voyageRepository = new GenericRepository<Voyage>(context);
                }
                return voyageRepository;
            }
        }

        public GenericRepository<Photo> PhotoRepository
        {
            get
            {

                if (this.photoRepository == null)
                {
                    this.photoRepository = new GenericRepository<Photo>(context);
                }
                return photoRepository;
            }
        }


        public GenericRepository<Categorie> CategorieRepository
        {
            get
            {

                if (this.categorieRepository == null)
                {
                    this.categorieRepository = new GenericRepository<Categorie>(context);
                }
                return categorieRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}