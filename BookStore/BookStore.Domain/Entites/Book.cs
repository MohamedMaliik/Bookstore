using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BookStore.Domain.Entites

{

     

    public class Book
    {
        [Key]
         
       public int ISBN { set; get; }
       
        [Required (ErrorMessage ="Please Enter Title the Book")]
       public string Title { set; get; }
       [Required(ErrorMessage ="enter book descrption")]
       public string Descrption { set; get; }
        [Required(ErrorMessage = "enter book Price ")]
        [Range(0.1,9999.99,ErrorMessage ="Please enter a positive price")]
        public decimal Price { set; get; }
       [Required(ErrorMessage ="Please enter specializaion")]
       public string Specialization { get; set; }
       
      
       
    }
}
