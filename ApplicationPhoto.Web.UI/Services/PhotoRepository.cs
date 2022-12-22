
using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ApplicationPhoto.Web.UI.Services
{
    public class RepositoryPhoto : IRepository<Voyage>
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<Voyage> dbSet;
        public RepositoryPhoto(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<Voyage>();
        }

        public IEnumerable<Voyage> GetAll()
        {
            return (IEnumerable<Voyage>)_context.Voyage.ToList();
        }

        public Voyage GetById(int id)
        {
            return _context.Voyage.Find(id);
        }

        public void Add(Voyage entity)
        {
            _context.Voyage.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Voyage entity)
        {
            _context.Voyage.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Voyage entity)
        {
            _context.Voyage.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Voyage> Get(List<Expression<Func<Voyage, bool>>> filter = null)
        {
            IQueryable<Voyage> query = dbSet;
            if (filter != null)
            {
                foreach (var filterExpression in filter)
                {
                    query = query.Where(filterExpression);
                }

            }
            return query.ToList();
        }

        public bool Exist(Voyage entity)
        {
            if (dbSet.Find(entity) != null)
            {
                return true;
            }
            return false;
        }
    }
}
