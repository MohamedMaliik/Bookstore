using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BookStore.WebUI.Infarsrturcter.Abstract;
using BookStore.WebUI.Models;
using BookStore.WebUI.Controllers;
using System.Web.Mvc;

namespace BookStore.UnitTest
{
    [TestClass]
    public class AdminSecurityTest
    {

        [TestMethod]
        public void Can_Login_With_Valid_Credentoals()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenicate("admin", "secret")).Returns(true);
            LoginViewModel model = new LoginViewModel
            {
                Username = "admin",
                Password = "secret"
                            


            };
            AccountController target = new AccountController(mock.Object);
            //Act
            ActionResult result = target.Login(model,"/MyUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual( "/MyUrl", ((RedirectResult)result).Url);

        }




        [TestMethod]
        public void Can_Login_With_InValid_Credentoals()
        {
            //Arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenicate("UserX", "Passx")).Returns(false);
            LoginViewModel model = new LoginViewModel
            {
                Username = "admin",
                Password = "secret"



            };
            AccountController target = new AccountController(mock.Object);
            //Act
            ActionResult result = target.Login(model, "/MyUrl");

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);

        }
    }
}
