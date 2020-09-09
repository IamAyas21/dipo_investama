using System.Web;

namespace DIPO_INVESTAMA.Utils
{
    public class SessionManager
    {
        public static string userId()
        {
            string value = string.Empty;
            try
            {
                value = HttpContext.Current.Session["userid"].ToString();
            }
            catch { }

            return value;
        }
    }
}