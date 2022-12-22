using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationPhoto.Web.UI.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
       
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(List<Expression<Func<T, bool>>> filter = null);
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        bool Exist(T entity);

    }

}
