using ApplicationPhoto.Web.UI.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationPhoto.Web.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Principal;
using ApplicationPhoto.Web.UI.Models;
using ApplicationPhoto.Web.UI.Services.Interfaces;

namespace ApplicationPhoto.Web.UI.xUnit
{
    public class VoyageControllerTest
    {
       

        public readonly DbContextOptions<ApplicationDbContext> dbContextOptions;

        public VoyageControllerTest()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "AventureDatabase")
                .Options;
        }
        [Fact]
        public async Task CreateReturnsARedirectToCreatePhotoWhenModelNotIsNull()
        {

           
           
            ////// Arrange
            ////var controller = new VoyageController();
            ////var voyage = new Mock<Voyage>();
            ////voyage.Object.IdUser = "xxxxx";
            ////voyage.Object.NomVoyage = "xxxxx";
            ////voyage.Object.DescriptionVoyage = "xxxxx";

            ////// Act
            ////var result = await  controller.Create(voyage.Object);

            ////// Assert
            ////var redirectToActionResult =
            ////    Assert.IsType<RedirectToActionResult>(result);
           
            ////Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
