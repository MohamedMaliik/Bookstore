using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books {get; set;}
        public PagingInfo PagingInfo { set; get; }
        public string currentSpeciliztion { set; get; }
    }
}