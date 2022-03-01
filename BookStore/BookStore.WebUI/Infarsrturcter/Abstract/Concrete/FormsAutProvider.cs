using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BookStore.WebUI.Infarsrturcter.Abstract.Concrete
{
    public class FormsAutProvider : IAuthProvider
    {
        public bool Authenicate(string username, string password)
        {

            bool result = FormsAuthentication.Authenticate (username  , password);

            if (result)
                FormsAuthentication.SetAuthCookie(username, false);
            return result;

        }
    }
}