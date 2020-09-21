using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class RoleBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static RoleBusinessLogic roleBusinessLogic = null;
        public static RoleBusinessLogic getInstance()
        {
            if (roleBusinessLogic == null)
            {
                roleBusinessLogic = new RoleBusinessLogic();
                return roleBusinessLogic;
            }
            else
            {
                return roleBusinessLogic;
            }
        }

        public PagedList<sp_RoleSelect_Result> ListRole()
        {
            var page = new PagedList<sp_RoleSelect_Result>();
            page.Content = _db.sp_RoleSelect().ToList();
            return page;
        }
        public int CreateRole(sp_RoleSelect_Result model)
        {
            return _db.sp_RoleCreate(model.RoleName,model.Maker, model.Checker,model.Approval,model.SuperUser, SessionManager.userId());
        }
        public int UpdateRole(sp_RoleSelect_Result model)
        {
            return _db.sp_RoleUpdate(model.RoleId, model.RoleName, model.Maker, model.Checker, model.Approval, model.SuperUser, SessionManager.userId());
        }

        public Role getRoleById(string id)
        {
            return _db.Roles.Where(b => b.RoleId.Equals(id)).FirstOrDefault();
        }

        public int DeleteRole(string id)
        {
            return _db.sp_RoleDelete(id);
        }

        public DataTable getRoleDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_RoleDDL())
            {
                DataRow row = tbl.NewRow();
                row["ID"] = item.ID;
                row["NAME"] = item.NAME;
                tbl.Rows.Add(row);
            }

            return tbl;
        }
    }
}