using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class MenuRestrictionsBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static MenuRestrictionsBusinessLogic menuRestrictionBusinessLogic = null;
        public static MenuRestrictionsBusinessLogic getInstance()
        {
            if (menuRestrictionBusinessLogic == null)
            {
                menuRestrictionBusinessLogic = new MenuRestrictionsBusinessLogic();
                return menuRestrictionBusinessLogic;
            }
            else
            {
                return menuRestrictionBusinessLogic;
            }
        }

        public PagedList<sp_PrivilegeSelect_Result> ListMenuRestriction()
        {
            var page = new PagedList<sp_PrivilegeSelect_Result>();
            page.Content = _db.sp_PrivilegeSelect().ToList();
            return page;
        }
    }
}