using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entites
{
    public class ShppingDetails
    {


        [Required(ErrorMessage ="Plese entr a name")]
        public string Name { set; get; }

        [Required(ErrorMessage ="Please enter the first address line")]
        [Display(Name="Address Line 1")]
        public string Line1 { set; get; }

        [Display(Name = "Address Line 2")]
        public string Line2 { set; get; }

        [Required (ErrorMessage ="Please enter your name city now")]
        public string City { set; get;}

        [Required(ErrorMessage ="please enter the city")]
        public string State { set; get; }

        [Required(ErrorMessage ="please enter the name city")]
        public string Country { set; get; }

        public bool GiftWrap { set; get; }


    }
}
