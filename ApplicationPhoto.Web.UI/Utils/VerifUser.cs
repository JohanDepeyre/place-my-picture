using ApplicationPhoto.Web.UI.Data.Migrations;
using Microsoft.AspNet.Identity;

namespace ApplicationPhoto.Web.UI.Utils
{
    public static class VerifUser
    {
        public static bool UserConnect(string idUser, string userConnect)
        {
            if (userConnect == idUser)
            { 
                return true;
            }

            return false;
            

        }
    }
}
