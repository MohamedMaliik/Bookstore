using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {

        public RedirectToRouteResult AddToCart(Cart cart,int isbn , string returnU)
        {
            Book book = repository.Books
                .FirstOrDefault( b => b.ISBN==isbn );
            if (book != null)
            {
                cart.Addltem(book);
            }
            return RedirectToAction("Index", new { returnU });//index
        }

       public RedirectToRouteResult RemoveFromCart (Cart cart,int isbn , string returnU)
        {

            Book book = repository.Books
                .FirstOrDefault(b => b.ISBN == isbn);
            if (book != null)
            {
               cart.Removeline(book);
            }
            return RedirectToAction("Index", new { returnU });
        }
        //private Cart GetCart()
        //{
        //    Cart cart =  (Cart) Session["Cart"];
        //    if (cart == null)
        //    {
        //        cart = new Cart();
        //        Session["Cart"] = cart;
                
        //    }
        //    return cart;
        //}

        private IBookRepository repository;
        private IOrderProcessor iorderProcessor;
       public  CartController (IBookRepository repo , IOrderProcessor iord)
        {
            repository = repo;
            iorderProcessor = iord;
        }

        public ViewResult Index( Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart =  cart, ReturnUrl = returnUrl

                                 } );
        }


        public PartialViewResult  Summary (Cart cart)
        {

            return PartialView(cart);

        }

         
        public ViewResult Checkout ()
        {
            return View(new ShppingDetails());

        }
        [HttpPost]

        public ViewResult Checkout( Cart cart,ShppingDetails shppingDetails)
        {

            if (cart.lines.Count() == 0)
                ModelState.AddModelError("", "Sorry is your Cart Empty");

            if (ModelState.IsValid)
            {
                iorderProcessor.ProcessorOrder(cart, shppingDetails);
                cart.Clear();
                return View("Completed");

            }
            else
                return View(shppingDetails);
        


        }
    }

}