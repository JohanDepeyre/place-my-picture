using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApplicationPhoto.Web.UI.DAL
{
    public class VoyageRepository:  IDisposable
    {
        private ApplicationDbContext context;

        public VoyageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Voyage>> GetVoyagesAsync()
        {
            return await context.Voyage.ToListAsync();
        }

        public Voyage GetVoyageByID(int id)
        {
            return context.Voyage.FirstOrDefault(x => x.VoyageId == id) ;
        }

        public void InsertVoyage(Voyage voyage)
        {
            context.Voyage.Add(voyage);
        }

        public void DeleteVoyage(Voyage voyage)
        {
           
            context.Voyage.Remove(voyage);
        }

        public void UpdateVoyage(Voyage student)
        {
            context.Entry(student).State = EntityState.Modified;
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
