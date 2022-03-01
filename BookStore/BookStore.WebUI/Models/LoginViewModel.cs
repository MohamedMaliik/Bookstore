using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { set; get;}
        [Required]
        public string Password { set; get; }
    }
}