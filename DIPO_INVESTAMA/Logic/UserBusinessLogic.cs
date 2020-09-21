using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
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

        public PagedList<sp_UserSelect_Result> ListUsers()
        {
            var page = new PagedList<sp_UserSelect_Result>();
            page.Content = _db.sp_UserSelect().ToList();
            return page;
        }

        public int CreateUser(sp_UserSelect_Result model)
        {
            return _db.sp_UserCreate(model.UserName,model.Password,model.Name, model.RoleId, model.DepartmentId, SessionManager.userId());
        }
        public int UpdateUser(sp_UserSelect_Result model)
        {
            return _db.sp_UserUpdate(model.UserId, model.UserName, model.Password, model.Name, model.RoleId, model.DepartmentId, SessionManager.userId());
        }

        public User getUserById(string id)
        {
            return _db.Users.Where(b => b.UserId.Equals(id)).FirstOrDefault();
        }

        public int DeleteUser(string id)
        {
            return _db.sp_UserDelete(id);
        }
    }
}