using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Data.Migrations;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationPhoto.Web.UI.Services
{
    public class PhotoRepository:IRepository<Photo>
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<Photo> dbSet;
        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<Photo>();
        }

        public IEnumerable<Photo> GetAll()
        {
            return _context.Photos.ToList();
        }

        public Photo GetById(int id)
        {

            return _context.Photos.Find(id);
        }
        public Photo GetByIdInclude(int id, string includeProperties = "")
        {
            IQueryable<Photo> query = dbSet;

           // query.Include(s => s.Categorie);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.First(x=>x.PhotoId ==id);
        }
        public void Add(Photo entity)
        {
            _context.Photos.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Photo entity)
        {
            _context.Photos.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Photo entity)
        {
            _context.Photos.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Photo> Get(List<Expression<Func<Photo, bool>>> filter = null)
        {
            IQueryable<Photo> query = dbSet;
            if (filter != null)
            {
                foreach (var filterExpression in filter)
                {
                    query = query.Where(filterExpression);
                }

            }
            return query.ToList();
        }

        public bool Exist(Photo entity)
        {
            if (dbSet.Find(entity) != null)
            {
                return true;
            }
            return false;
        }

      
    }
}
