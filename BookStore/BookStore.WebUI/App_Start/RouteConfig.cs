using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Inconig
            routes.MapRoute(
                null
              ,
                "" , new {
                    controller = "Book" ,Action ="List" ,
                    speciliztion =(string) null  , pageeno =1
                }
                 
          );
             
            routes.MapRoute(
                null
              , //URl/Information Systeam 
                "BookListPage{pageeno}", new
                {
                    controller = "Book",
                    Action = "List",
                    speciliztion = (string)null,
                    
                }
                ,new{pageeno = @"\d+"}
          );
             
            routes.MapRoute(
                null
              ,
                "{speciliztion}", new
                {
                    controller = "Book",Action ="List",
                  
                    pageeno = 1
                }

          );
             
            routes.MapRoute(
              name:  null,
              url: "BookListPage{pageeno}",
              defaults: new { controller = "Book", action = "List", id = UrlParameter.Optional } 

          );
             
            routes.MapRoute(
                null
              ,
                "{speciliztion}/page{pageeno}", new
                {
                    controller = "Book",
                    Action = "List",

                    pageeno = 1
                }

          );
             
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {  id = UrlParameter.Optional }
            );
        }
    }
}
