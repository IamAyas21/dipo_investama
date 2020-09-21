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
    public class MenuRestrictionBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static MenuRestrictionBusinessLogic menuRestrictionBusinessLogic = null;
        public static MenuRestrictionBusinessLogic getInstance()
        {
            if (menuRestrictionBusinessLogic == null)
            {
                menuRestrictionBusinessLogic = new MenuRestrictionBusinessLogic();
                return menuRestrictionBusinessLogic;
            }
            else
            {
                return menuRestrictionBusinessLogic;
            }
        }

        public PagedList<sp_MenuRestrictionSelect_Result> ListMenuRestriction()
        {
            var page = new PagedList<sp_MenuRestrictionSelect_Result>();
            page.Content = _db.sp_MenuRestrictionSelect().ToList();
            return page;
        }
        public int CreateMenuRestriction(sp_MenuRestrictionSelect_Result model)
        {
            return _db.sp_MenuCreate(model.MenuId, model.RoleId, SessionManager.userId());
        }
        public int UpdateMenuRestriction(sp_MenuRestrictionSelect_Result model)
        {
            return _db.sp_MenuUpdate(model.MenuId, model.MenuId, model.RoleId, SessionManager.userId());
        }

        public MenuRestriction getMenuById(string id)
        {
            return _db.MenuRestrictions.Where(b => b.RestrictionId.Equals(id)).FirstOrDefault();
        }

        public int DeleteMenuRestriction(string id)
        {
            return _db.sp_MenuDelete(id);
        }

        public DataTable getMenuDDL(string roleId)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_MenuDDL(roleId))
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