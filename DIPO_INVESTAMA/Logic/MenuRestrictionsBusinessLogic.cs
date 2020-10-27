using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
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

        public void InsertRole(string roleName)
        {
            DIPO_INVESTAMAEntities db = new DIPO_INVESTAMAEntities();
            Role role = new Role();
            role.RoleId = Guid.NewGuid().ToString();
            role.RoleName = roleName;
            role.CreatedAt = DateTime.Now;
            role.CreatedBy = SessionManager.userId();
            db.Roles.Add(role);
            db.SaveChanges();
        }

        public void UpdateMenuRestriction(MenuRestriction menuRestriction)
        {
            _db.sp_PrivilegeUpdate(menuRestriction.RestrictionId, SessionManager.userId(), menuRestriction.RoleId, menuRestriction.MenuId, menuRestriction.IsRead);
        }

        public void UpdateRole(string id, string roleName)
        {
            (from p in _db.Roles
             where p.RoleId == id
             select p).ToList()
                                         .ForEach(x => x.RoleName = roleName);

            _db.SaveChanges();
        }

        public List<MenuViewModels> SaveMenu(List<MenuViewModels> DataMenu, string privilegeId)
        {
            if (DataMenu != null)
            {
                foreach (var item in DataMenu)
                {
                    DIPO_INVESTAMAEntities _dbNew = new DIPO_INVESTAMAEntities();
                    InsertMenuRestrictions(privilegeId, item.Parent.MenuId, item.Checked);
                    SaveMenu(item.Child, privilegeId);
                }
            }

            return DataMenu;
        }

        private void InsertMenuRestrictions(string privilegeId, string menuId, bool isChecked)
        {
            DIPO_INVESTAMAEntities db = new DIPO_INVESTAMAEntities();
            MenuRestriction menuResctriction = new MenuRestriction();
            menuResctriction.RestrictionId = Guid.NewGuid().ToString();
            menuResctriction.RoleId = privilegeId;
            menuResctriction.MenuId = menuId;
            menuResctriction.IsRead = isChecked;
            menuResctriction.CreatedAt = DateTime.Now;
            menuResctriction.CreatedBy = SessionManager.userId();
            db.MenuRestrictions.Add(menuResctriction);
            db.SaveChanges();
        }

        public List<Menu> GetPrivilegeTree(string userId, string privilegeId, string parentId)
        {
            List<Menu> listMenu = new List<Menu>();
            Menu menu = new Menu();
            var list = _db.sp_PrivilegeTree(userId, privilegeId, parentId).ToList();
            foreach(var item in list)
            {
                menu = new Menu();
                menu.MenuId = item.MenuId;
                menu.ParentId = item.ParentId;
                menu.MenuName = item.MenuName;
                menu.Controller = item.Controller;
                menu.Action = item.Action;
                menu.Icon = item.Icon;
                menu.Opt = item.Opt;
                menu.ShowMenu = item.ShowMenu;
                menu.Descriptions = item.Descriptions;
                menu.CreatedAt = item.CreatedAt;
                menu.CreatedBy = item.CreatedBy;
                menu.UpdatedAt = item.UpdatedAt;
                menu.UpdatedBy = item.UpdatedBy;
                menu.IsRead = item.IsRead;
                listMenu.Add(menu);
            }
            return listMenu;
        }
    }
}