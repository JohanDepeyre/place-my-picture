using ApplicationPhoto.Web.UI.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Helpers;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ApplicationPhoto.Web.UI.DAL;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ApplicationPhoto.Web.UI.Data.Migrations;
using Microsoft.AspNet.Identity;
using Voyage = ApplicationPhoto.Web.UI.Models.Voyage;
using ApplicationPhoto.Web.UI.Utils.ImageTraitement;

namespace ApplicationPhoto.Web.UI.Controllers
{
    public class PhotoController : Controller
    {
     
        private readonly IWebHostEnvironment _env;
        private readonly UnitOfWork unitOfWork;
        private string userConnect;
        public PhotoController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            ApplicationDbContext _context = context;
            unitOfWork = new UnitOfWork(_context);
            _env = env;
           
        }


        // GET: PictureController/Index/3
        [Authorize]
        [HttpGet]
        public ActionResult Index(int id)
        {
            userConnect = User.Identity.GetUserId();
            return View(unitOfWork.PhotoRepository.Get(FilterPhotoById(id)));
        }
        [Authorize]
        public ActionResult Index()
        {


            return View();
        }
        #region Détails

        [Authorize]
        // GET: PictureController/Details/5
        public ActionResult Details(int id)
        {
            userConnect = User.Identity.GetUserId();
            if (userConnect == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var photo = unitOfWork.PhotoRepository.GetByID((int)id);
            photo.Voyage  = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);
            photo.Categorie = unitOfWork.CategorieRepository.GetByID(photo.CategorieId);
            if (photo == null || !VerifUser.UserConnect(photo.Voyage.IdUser, userConnect))
            {
                return NotFound();
            }
            return View(photo);
        }
        #endregion

        #region Create
        [Authorize]
        // GET: PictureController/Create
        public ActionResult Create()
        {
            ViewBagPictureCreate("",true,true);
            ViewBagCategorie();
            ViewBagVoyage();
            return View();
        }
        [Authorize]
        // POST: PictureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo)
        {
            //on suppose que les photos dispose d'EXIF (latitude/longitude) et (d'un date)
            bool flagLatLon = true;
            bool flagDate = true;
            //refill si on recharge la page si pas de métadonné
            ViewBagCategorie(photo.VoyageId);
            ViewBagVoyage(photo.CategorieId);

            photo.Voyage  = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);
            photo.Categorie = unitOfWork.CategorieRepository.GetByID(photo.CategorieId);

            if (this.ModelState.IsValid)
            {


                if (photo.MyImage != null && photo.MyImage.Length > 0)
                {

                    ImageO monImage = new ImageO();
                    ImageSaver imageSaver = new ImageSaver(monImage,photo,_env);

                    ImageProcessor imageProcessor = new ImageProcessor(imageSaver, new ImageDeleter(),new ImageGetter());

                   
                   
                    Image image = imageProcessor.ProcessSave();
#pragma warning disable CS8629 // Le type valeur Nullable peut avoir une valeur null.
                    photo.Latitude = GestionPropertyItem.GetLatitude(image);
                    photo.Longitude = GestionPropertyItem.GetLongitude(image);
                    if (photo.Latitude == null)
                    {
                        flagLatLon = false;
                    }
                    photo.ImageUrl = monImage.filePathDb;

                    photo.Name = monImage.uniqueFileName;
               
             

                    photo.DatePicture = GestionPropertyItem.ReturnDate(image);

                    if (photo.DatePicture == null)
                    {
                        flagDate = false;
                        imageProcessor.ProcessDelete(photo.ImageUrl);

                    }
                    //si un des deux flags est null alors on redirigise vers la vue pour afficher les panels adequats(ex: saisir la date)
                    if (!flagDate || !flagLatLon)
                    {
                        ViewBagPictureCreate("", flagLatLon, flagDate);
                        return View();
                    }
                
                    unitOfWork.PhotoRepository.Insert(photo);
                    unitOfWork.Save();


                }

                return RedirectToAction("Index", new { id = photo.VoyageId });

            }
            else
            {
                ViewBagPictureCreate("", flagLatLon, flagDate);

                return View();

            }

           
        }

        #endregion

        #region Edit
        [Authorize]
        // GET: PictureController/Edit/5
        public ActionResult Edit(int id)
        {
            userConnect = User.Identity.GetUserId();

            if (userConnect == null)
            {
                return RedirectToAction(nameof(Index));
            }
           
            var photo = unitOfWork.PhotoRepository.GetByID((int)id);
            photo.Voyage = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);
            if (photo == null || !VerifUser.UserConnect(photo.Voyage.IdUser, userConnect))
            {
                return NotFound();
            }
            ViewBagCategorie(photo.CategorieId);

            return View(photo);
        }
          [Authorize]
        // POST: PictureController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,Photo photo)
        {
            userConnect = User.Identity.GetUserId();
            photo.Voyage = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);

            if (id != photo.PhotoId || !VerifUser.UserConnect(photo.Voyage.IdUser, userConnect))
            {
                return NotFound();
            }

            if (photo != null)
            {
                try
                {
                    unitOfWork.PhotoRepository.Update(photo);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = photo.VoyageId });
            }
            return View(photo);
        }
        #endregion

        #region Delete
        [Authorize]
        // GET: Picture/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            userConnect = User.Identity.GetUserId();
            if (userConnect == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (id == null)
            {
                return NotFound();
            }
            
            var photo = unitOfWork.PhotoRepository.GetByID(id);
            photo.Voyage = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);
            if (photo == null || !VerifUser.UserConnect(photo.Voyage.IdUser, userConnect))
            {
                return NotFound();
            }

            return View(photo);
        }
        [Authorize]
        // POST: Picture/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            
            var photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo != null)
            {
                unitOfWork.PhotoRepository.Delete(photo);
                ImageDeleter imageDeleter = new ImageDeleter();

                imageDeleter.DeleteImage(Path.Combine(_env.WebRootPath, photo.ImageUrl));
            }

            unitOfWork.Save();
            return RedirectToAction("Index", new { id = photo.VoyageId });
        }

        #endregion

        #region Methode de filtre

        List<Expression<Func<Photo, bool>>> FilterPhotoById(int id)
        {

            List<Expression<Func<Photo, bool>>> expressions;
            expressions = new List<Expression<Func<Photo, bool>> >();
            expressions.Add(x => x.VoyageId == id);
            expressions.Add(x => x.Voyage.IdUser == userConnect);
              return expressions;
          

        }
        List<Expression<Func<Voyage, bool>>> FilterUser()
        {

            List<Expression<Func<Voyage, bool>>> listFilter = new List<Expression<Func<Voyage, bool>>>();
            listFilter.Add(x => x.IdUser == User.Identity.GetUserId());



            return listFilter;
        }
        #endregion

        #region Methodes privées

        private bool PhotoExists(Photo photo)
        {
            return unitOfWork.PhotoRepository.Exists(photo);
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }

        private void ViewBagCategorie(int? selectedCategorie = null)
        {

            ViewBag.ListeCategorie = new SelectList(unitOfWork.CategorieRepository.Get(), "CategorieId", "NameCategorie", selectedCategorie);



        }
        private void ViewBagVoyage(int? selectedVoyage = null)
        {
            ViewBag.ListeVoyage = new SelectList(unitOfWork.VoyageRepository.Get(FilterUser()), "VoyageId", "NomVoyage", selectedVoyage);

        }
        /// <summary>
        /// Implémente les viewbag
        /// </summary>
        /// <param name="etat"></param>
        /// <param name="flagLatLon">Bool, si il est a false , affiche le panel pour saisir une adresse dans View=>Picture=>Create</param>
        /// <param name="flagDate">bool , si false alors affiche le panel Date dans View=>Picture=>Create</param>
        private void ViewBagPictureCreate(string etat, bool flagLatLon,bool flagDate)
        {
            ViewBag.EtatPhoto = etat;
            ViewBag.LatLon = flagLatLon;
            ViewBag.Date = flagDate;
        }

     




        #endregion
    }

}
