using BookStore.WebUI.Infarsrturcter.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {

        IAuthProvider authprovider;
        public AccountController (IAuthProvider atuo)
        {
            authprovider = atuo;

        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model , string returnurl)
        {
            if (ModelState.IsValid)
            {
                if (authprovider.Authenicate(model.Username, model.Password))
                    return Redirect(returnurl ?? Url.Action("Index", "Admin"));
                else
                {

                    ModelState.AddModelError("", "Incorrect Username/password");
                    return View();
                }
            }else
            return View();
        }
    }
}