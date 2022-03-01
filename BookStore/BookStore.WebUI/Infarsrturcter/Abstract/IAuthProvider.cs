using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.WebUI.Infarsrturcter.Abstract
{
  public  interface IAuthProvider
    {
        bool Authenicate(string username, string password);
    }
}
