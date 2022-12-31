
using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Text.Json;

namespace ApplicationPhoto.Web.UI.Controllers
{
    public class CarteController : Controller
    {
       
        
        private readonly IRepository<Photo> _photoRepository;
        private readonly IRepository<Categorie> _categorieRepository;
        private readonly IRepository<Voyage> _voyageRepository;
        private string userConnect;


        public CarteController(IRepository<Photo> photoRepository, IRepository<Categorie> categorieRepository, IRepository<Voyage> voyageRepository, IWebHostEnvironment env)
        {
            this.userConnect = "";
            this._photoRepository = photoRepository;
            this._categorieRepository = categorieRepository;
            this._voyageRepository = voyageRepository;
           

        }

        [Authorize]
        public IActionResult Index()
        {

            


            ViewBagCategorie();
            ViewBagVoyage();
           

            return View(_photoRepository.Get(Filter(null,null,null,null)));
        }
        // GET: PictureController/Index/3
        [Authorize]
        [HttpPost]
        public ActionResult Index(int? idCategorie, int? idVoyage, string? depart, string? fin )
        {
            ViewBagCategorie(idCategorie);
            ViewBagVoyage(idVoyage);
            return View(_photoRepository.Get(Filter(idCategorie, idVoyage,depart,fin)));


          
        }
        /// <summary>
        /// Contruit la liste contenant les critére de recherche
        /// </summary>
        /// <param name="idCategorie"></param>
        /// <param name="idVoyage"></param>
        /// <param name="depart"></param>
        /// <param name="fin"></param>
        /// <returns>liste d'Expression lambda contenant le filtre</returns>
        List<Expression<Func<Photo, bool>>> Filter(int? idCategorie, int? idVoyage, string? depart, string? fin)
        {

            List<Expression<Func<Photo, bool>>> listFilter = new List<Expression<Func<Photo, bool>>>();
            listFilter.Add(x => x.Voyage.IdUser == User.Identity.GetUserId());

            if (idVoyage != null)
            {
                listFilter.Add(x => x.VoyageId == idVoyage);
                
            }
            else if (idCategorie != null)
            {
                listFilter.Add(x => x.CategorieId == idCategorie);
              
            }
            else if (depart != null && fin !=null)
            {
               listFilter.Add(x => (x.DatePicture > Convert.ToDateTime(depart) && x.DatePicture < Convert.ToDateTime(fin)));
            }
            else if (depart != null && fin == null)
            {
                listFilter.Add(x => x.DatePicture > Convert.ToDateTime(depart));
            }
            else if (depart == null && fin != null)
            {
                listFilter.Add(x => x.DatePicture < Convert.ToDateTime(fin));
            }

            return listFilter;
        }

      



        private void ViewBagCategorie(int? selectedCategorie = null)
        {

            ViewBag.ListeCategorie = new SelectList(_categorieRepository.GetAll(), "CategorieId", "NameCategorie", selectedCategorie);

           

        }
        private void ViewBagVoyage(int? selectedVoyage = null)
        {
            ViewBag.ListeVoyage = new SelectList(_voyageRepository.Get(FilterUser()), "VoyageId", "NomVoyage", selectedVoyage); 

        }

        List<Expression<Func<Voyage, bool>>> FilterUser()
        {

            List<Expression<Func<Voyage, bool>>> listFilter = new List<Expression<Func<Voyage, bool>>>();
            listFilter.Add(x => x.IdUser == User.Identity.GetUserId());



            return listFilter;
        }

    }
}
