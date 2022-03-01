using BookStore.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Infarsrturcter
{

    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel( ControllerContext ControllerContext,
            ModelBindingContext bindingContext)
        {

            Cart cart = null;
            if (ControllerContext.HttpContext.Session != null)
                cart = (Cart)ControllerContext.HttpContext.Session[sessionKey];
             if(cart == null)
            {
                cart = new Cart();
                if (ControllerContext.HttpContext.Session != null)
                    ControllerContext.HttpContext.Session[sessionKey] = cart;
            }

            return cart ;

        }

        
    }
}