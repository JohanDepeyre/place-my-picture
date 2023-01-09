using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces.Generic;
using System.Linq.Expressions;

namespace ApplicationPhoto.Web.UI.Services.Interfaces
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        public IEnumerable<Photo> GetPagedData(int page, int pageSize, List<Expression<Func<Photo, bool>>> filter = null);
        public int GetTotalPages(int idVoyage, int pageSize);

    }
}
