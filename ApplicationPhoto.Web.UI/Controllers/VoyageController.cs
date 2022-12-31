using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationPhoto.Web.UI.Data;
using ApplicationPhoto.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNet.Identity;
using ApplicationPhoto.Web.UI.Utils;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ApplicationPhoto.Web.UI.Services.Interfaces;

namespace ApplicationPhoto.Web.UI.Controllers
{
    public class VoyageController : Controller
    {
       
 
        private string userConnect;

        private readonly IRepository<Voyage> _voyageRepository;


        public VoyageController(IRepository<Voyage> voyageRepository)
        {
            this.userConnect = "";
            this._voyageRepository = voyageRepository;
        }

        [Authorize]
        // GET: Voyage
        public async Task<IActionResult> Index()
        {
            userConnect = User.Identity.GetUserId();
            var voyage = _voyageRepository.Get(Filter());
            return View (voyage);
        }


        [Authorize]
        // GET: Voyage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            userConnect = User.Identity.GetUserId();
            if (userConnect == null)
            {
                return NotFound();
            }

            if (id == null )
            {
                return NotFound();
            }

            var voyage = _voyageRepository.GetById((int)id);
            if (voyage == null || !VerifUser.UserConnect(voyage.IdUser, userConnect) )
            {
                return NotFound();
            }

            return View(voyage);
        }
        [Authorize]
        // GET: Voyage/Create
        public IActionResult Create()
        {
            userConnect = User.Identity.GetUserId();
            if (userConnect == null)
            {
                return RedirectToAction(nameof(Index));
            }
            Voyage voyage = new Voyage();
            voyage.DateVoyageDebut = DateTime.Today;
            voyage.DateVoyageFin = DateTime.Today;
            voyage.IdUser = User.Identity.GetUserId();
            return View(voyage);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoyageId,NomVoyage,DateVoyageDebut,DateVoyageFin,DescriptionVoyage,IdUser")] Voyage voyage)
        {
            
            if (ModelState.IsValid)
            {

                if (voyage !=null)
            {

                 _voyageRepository.Add(voyage);
               
                return RedirectToAction(nameof(Index));
            }

            }
            return View(voyage);
        }
        [Authorize]
        // GET: Voyage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            userConnect = User.Identity.GetUserId();
            if (userConnect == null)
            {
                return NotFound();
            }
            if (id == null  )
            {
                return NotFound();
            }

            var voyage = _voyageRepository.GetById((int)id);
            if (voyage == null || !VerifUser.UserConnect(voyage.IdUser, userConnect))
            {
                return NotFound();
            }
            return View(voyage);
        }

        // POST: Voyage/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("VoyageId,NomVoyage,DateVoyageDebut,DateVoyageFin,DescriptionVoyage")] Voyage voyage)
        {
            userConnect = User.Identity.GetUserId();
            if (id != voyage.VoyageId || !VerifUser.UserConnect(voyage.IdUser, userConnect))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _voyageRepository.Update(voyage);
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoyageExists(voyage))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(voyage);
        }
        [Authorize]
        // GET: Voyage/Delete/5
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

            var voyage = _voyageRepository.GetById((int)id);

            if (voyage == null|| !VerifUser.UserConnect(voyage.IdUser,userConnect))
            {
                return NotFound();
            }

            return View(voyage);
        }
        [Authorize]
        // POST: Voyage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            userConnect = User.Identity.GetUserId();
            var voyage = _voyageRepository.GetById(id);
            if (voyage!= null || !VerifUser.UserConnect(voyage.IdUser, userConnect))
            {
                _voyageRepository.Delete(voyage);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Constrution de l'expression de filtre
        /// </summary>
        /// <returns>liste expression lambda</returns>
        List<Expression<Func<Voyage, bool>>> Filter()
        {

            List<Expression<Func<Voyage, bool>>> listFilter = new List<Expression<Func<Voyage, bool>>>();
            listFilter.Add(x => x.IdUser == User.Identity.GetUserId());

         

            return listFilter;
        }
        private bool VoyageExists(Voyage voyage)
        {
          return _voyageRepository.Exist(voyage);
        }

 

        
    }
}
