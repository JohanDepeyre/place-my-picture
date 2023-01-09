using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationPhoto.Web.UI.Services
{
    public class CategorieRepository:IRepository<Categorie>
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<Categorie> dbSet;
        public CategorieRepository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = context.Set<Categorie>();
        }

        public IEnumerable<Categorie> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Categorie GetById(int id)
        {
            return _context.Categories.Find(id);
        }
        public Categorie GetByIdInclude(int id, string includeProperty = "")
        {
            throw new NotImplementedException();
        }
        public void Add(Categorie entity)
        {
            _context.Categories.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Categorie entity)
        {
            _context.Categories.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(Categorie entity)
        {
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Categorie> Get(List<Expression<Func<Categorie, bool>>> filter = null)
        {
            IQueryable<Categorie> query = dbSet;
            if (filter != null)
            {
                foreach (var filterExpression in filter)
                {
                    query = query.Where(filterExpression);
                }

            }
            return query.ToList();
        }

        public bool Exist(Categorie entity)
        {
            if (dbSet.Find(entity) != null)
            {
                return true;
            }
            return false;
        }

       
    }
}

