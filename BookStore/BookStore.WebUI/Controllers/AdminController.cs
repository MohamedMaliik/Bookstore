using BookStore.Domain.Abstract;
using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IBookRepository repository;
        public AdminController (IBookRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
          
            return View(repository.Books);

        }
        public ViewResult Edit (int ISBN)
        {
            Book book = repository.Books.FirstOrDefault(b => b.ISBN == ISBN);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if(ModelState.IsValid)
            {
                repository.SaveBook(book);

                TempData["message"] = book.Title + "Has Saved";

                return RedirectToAction("Index");


            }
            else
            {
                //not
            }
            return View(book);
        }
       public ViewResult Create()
        {
        

            return View("Edit", new Book());


        }
 
        [HttpGet]
        public ActionResult  Delete(int ISBN)
        {
            Book deleteBook = repository.DeleteBook(ISBN);

            if(deleteBook !=null)
            {
                TempData["message"] = deleteBook.Title + "Was deleted"; 
            }
            return RedirectToAction("Index");
        {


            }

        }

    }
}