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
using ApplicationPhoto.Web.UI.Services;
using ApplicationPhoto.Web.UI.Services.Interfaces;
using ApplicationPhoto.Web.UI.Models.ViewModel;

namespace ApplicationPhoto.Web.UI.Controllers
{
    public class PhotoController : Controller
    {
     
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<Photo> _photoRepository;
        private readonly IRepository<Categorie> _categorieRepository;
        private readonly IRepository<Voyage> _voyageRepository;
        private string userConnect;


        public PhotoController(IRepository<Photo> photoRepository, IRepository<Categorie> categorieRepository, IRepository<Voyage> voyageRepository, IWebHostEnvironment env)
        {
            this.userConnect = "";
            this._photoRepository =photoRepository;
            this._categorieRepository = categorieRepository;
            this._voyageRepository = voyageRepository;
            _env = env;
           
        }


        // GET: PictureController/Index/3
        [Authorize]
        [HttpGet]
        public ActionResult Index(int id)
        {
            userConnect = User.Identity.GetUserId();
            return View(_photoRepository.Get(FilterPhotoById(id)));
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
            var photo = _photoRepository.GetByIdInclude((int)id, "Voyage,Categorie");
           // photo.Voyage  = .GetById.VoyageRepository.GetByID(photo.VoyageId, );
//photo.Categorie = unitOfWork.CategorieRepository.GetByID(photo.CategorieId);
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
            ViewBagPictureCreate("", true, true);
            ViewBagCategorie();
            ViewBagVoyage();
            

            return View ();
        }
        [Authorize]
        // GET: PictureController/CreateMultiple
        public ActionResult CreateMultiple()
        {

            ViewBagVoyage();
            return View();
        }

        [Authorize]
        // POST: PictureController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilesPhotoViewModel fichier)
        {
           // Photo photo = fichier.Photos;
           
           

           
                
                
                int nbrImages = ContientImage(fichier.Files);

                if (nbrImages  > 0)
                {

                foreach (var file in fichier.Files)
                {
                    Photo photo = new Photo
                    {
                        VoyageId = fichier.Photos.VoyageId,
                        CategorieId = fichier.Photos.CategorieId,
                        Description = fichier.Photos.Description

                    };
                        photo.MyImage = file;
                        CreationObjetPhoto(photo);

                        //si il manque une date ou la latitude/longitude on ouvre un panel ( pas dispo pour plusieurs photo)
                        if (!NoExifLatLonDate(photo))
                        {
                            ViewBagPictureCreate("", false, false);
                            return View();
                        }

                        _photoRepository.Add(photo);
                    }


            }
            else
            {
                ViewBagCategorie(fichier.Photos.VoyageId);
                ViewBagVoyage(fichier.Photos.CategorieId);
                return View();
            }
            //refill si on recharge la page si pas de métadonné
           
            return RedirectToAction("Index", new { id = fichier.Photos.VoyageId });

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
           
            var photo = _photoRepository.GetByIdInclude((int)id,"Voyage,Categories");
            //photo.Voyage = unitOfWork.VoyageRepository.GetByID(photo.VoyageId);
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
            photo.Voyage = _voyageRepository.GetById(photo.VoyageId);

            if (id != photo.PhotoId || !VerifUser.UserConnect(photo.Voyage.IdUser, userConnect))
            {
                return NotFound();
            }

            if (photo != null)
            {
                try
                {
                    _photoRepository.Update(photo);
                   
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
            
            var photo = _photoRepository.GetByIdInclude((int)id,"Voyage,Categorie");
            //photo.Voyage = _voyageRepository.GetById(photo.VoyageId);
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

            
            var photo = _photoRepository.GetById(id);
            if (photo != null)
            {
                _photoRepository.Delete(photo);
                

                ImageDeleter.DeleteImage(Path.Combine(_env.WebRootPath, photo.ImageUrl));
            }

        
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
        private int ContientImage(List<IFormFile> files)
        {
            return files.Count();
        }

        private Photo CreationObjetPhoto(Photo photo)
        {

            ImageO monImage = new ImageO();
            ImageSaver imageSaver = new ImageSaver(monImage, photo, _env);
            Image image = imageSaver.SaveImage();

            photo.Voyage = _voyageRepository.GetById(photo.VoyageId);
            photo.Categorie = _categorieRepository.GetById(photo.CategorieId);
#pragma warning disable CS8629 // Le type valeur Nullable peut avoir une valeur null.
            photo.Latitude = GestionPropertyItem.GetLatitude(image);
            photo.Longitude = GestionPropertyItem.GetLongitude(image);

            photo.ImageUrl = monImage.filePathDb;

            photo.Name = monImage.uniqueFileName;



            photo.DatePicture = GestionPropertyItem.ReturnDate(image);
            return photo;
        }
        private bool NoExifLatLonDate(Photo photo)
        {

            if (photo.Latitude == null || photo.DatePicture == null)
            {
                ImageDeleter.DeleteImage(photo.ImageUrl);
                return false;
            }


            return true;
        }
        private bool PhotoExists(Photo photo)
        {
            return _photoRepository.Exist(photo);
        }

      

        private void ViewBagCategorie(int? selectedCategorie = null)
        {

            ViewBag.ListeCategorie = new SelectList(_categorieRepository.GetAll(), "CategorieId", "NameCategorie", selectedCategorie);



        }
        private void ViewBagVoyage(int? selectedVoyage = null)
        {
            ViewBag.ListeVoyage = new SelectList(_voyageRepository.Get(FilterUser()), "VoyageId", "NomVoyage", selectedVoyage);

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
