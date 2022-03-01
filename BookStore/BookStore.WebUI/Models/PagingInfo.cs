using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{

    public class PagingInfo
    {

      public int totalitem { set; get; }
      public int itemperpage { set; get; }
      public int currentPage { get; set; }
 
      public int Totalpage { get { return (int) Math.Ceiling( (decimal) totalitem / itemperpage ); } }

    }
}