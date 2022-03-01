using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.HtmlHelper
{
    public static class PagingHelper
    {
         
        public static MvcHtmlString Pagelinks (
            this System.Web.Mvc.HtmlHelper html 
            , 
            PagingInfo pageInfo, Func<int, string > PageUrl )
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.Totalpage; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", PageUrl(i));
                tag.InnerHtml = i.ToString();
                //selected
                if (i == pageInfo.currentPage)
                {
                        //tag.AddCssClass("Select");
                    tag.AddCssClass(" btn btn-primary");
                 //   tag.AddCssClass("btn btn-default");
                }
                else

                    tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());

            }

            return MvcHtmlString.Create( result.ToString());
        }
        
    }
}