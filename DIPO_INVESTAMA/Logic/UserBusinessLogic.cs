using DIPO_INVESTAMA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class UserBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static UserBusinessLogic userBusinessLogic = null;
        public static UserBusinessLogic getInstance()
        {
            if (userBusinessLogic == null)
            {
                userBusinessLogic = new UserBusinessLogic();
                return userBusinessLogic;
            }
            else
            {
                return userBusinessLogic;
            }
        }
        public sp_UserLogin_Result UserLogin(string userName, string password)
        {
            try
            {
                return _db.sp_UserLogin(userName, password).FirstOrDefault();
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                throw e;
            }
        }
    }
}