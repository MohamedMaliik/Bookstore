using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
 {
    public class NavController : Controller
    {
        private IBookRepository repostiory;

        public NavController (IBookRepository repo)
        {
            repostiory = repo;
        }
        public PartialViewResult  Menu( string speciliztion = null)
        {
            ViewBag.Selecedsepc = speciliztion;
            IEnumerable<string> spec = repostiory.Books
                .Select(b => b.Specialization)
                .Distinct();

            return PartialView("FlexMenu", spec);

        }        
    }
 }

