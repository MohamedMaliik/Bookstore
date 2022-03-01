using  BookStore.Domain.Abstract;
using  System; 
using  System.Collections.Generic;
using  System.Linq;
using  System.Web;
using  System.Web.Mvc;
using  BookStore.Domain.Entites;
using  BookStore.WebUI.Models;



namespace BookStore.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repostiory;

        public BookController ( IBookRepository bookrep)
        {
            repostiory = bookrep;
        }
       public  int pagesize = 4;
         

        public ViewResult List ( string speciliztion , int pageeno = 1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repostiory.Books
                .Where(b => speciliztion == null || b.Specialization == speciliztion)
                .OrderBy(b => b.ISBN)
                .Skip((pageeno - 1) * pagesize).Take(pagesize)
                
            ,
                PagingInfo = new PagingInfo
                {
                    currentPage = pageeno,
                    itemperpage = pagesize,
                    totalitem = speciliztion == null ? repostiory.Books.Count() : repostiory.Books.Where(b => b.Specialization == speciliztion).Count()

                },
                currentSpeciliztion = speciliztion
                //   currentSpeciliztion = speciliztion ;

            };
             
                return View(model);            
        }

        public ActionResult ListAll()
        {
            return View(repostiory.Books);
        }

    }
}