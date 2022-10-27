using ApplicationPhoto.Web.UI.Models;

namespace ApplicationPhoto.Web.UI.DAL
{
    public interface GenericRepository : IDisposable
    {
        Task<IEnumerable<Object>> GetVoyagesAsync();
        Voyage GetVoyageByID(int VoyageId);
        void InsertVoyage(Object voyage);
        void DeleteVoyage(Object voyage);
        void UpdateVoyage(Object voyage);
        void Save();
       
    }
}
