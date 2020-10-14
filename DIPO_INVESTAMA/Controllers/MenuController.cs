using DIPO_INVESTAMA.Entity;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        public ActionResult Menu(string controller, string action)
        {
            var xData = (from x in _db.Menus
                         where x.Controller.ToUpper() == controller.ToUpper() && x.Action.ToUpper() == action.ToUpper()
                         select x);
            string ID1 = string.Empty, ID2 = string.Empty, ID3 = string.Empty;
            if (xData.ToList().Count() != 0)
            {
                ID1 = xData.Single().ParentId;
                ID2 = xData.Single().MenuId;
                var D3 = _db.Menus.Where(x => x.MenuId == ID1).FirstOrDefault();
                try
                {
                    ID3 = D3.ParentId;
                }
                catch { }

            }
            else
            {
                var d = (from x in _db.Menus
                         where x.Controller.ToUpper() == controller.ToUpper() && x.Action.ToUpper() == "INDEX"
                         select x);
                if (d.ToList().Count != 0)
                {
                    ID1 = d.Single().ParentId;
                    ID2 = d.Single().MenuId;
                    var D3 = _db.Menus.Where(x => x.MenuId == ID1).FirstOrDefault();
                    try
                    {
                        ID3 = D3.ParentId;
                    }
                    catch { }
                }

            }


            List<MenuViewModels> DT = DTMenu("0", ID1, ID2, ID3);
            MenuModels DTPartial = new Models.MenuModels { Menu = DT };
            //   (from x in dataBase.Customers
            //    where x.Name == "Test"
            //selext x).ToList().ForEach(xx => x.Name = "New Name");

            return PartialView("_Menu", DTPartial);
        }

        public List<MenuViewModels> DTMenu(string ParentIDValue, string ID1, string ID2, string ID3)
        {
            List<MenuViewModels> DATA = new List<MenuViewModels>();
            List<Menu> DT;

            string userId = SessionManager.userId();
            DT = (from mn in _db.Menus
                    join mr in _db.MenuRestrictions on mn.MenuId equals mr.MenuId
                    join usr in _db.Users on mr.RoleId equals usr.RoleId
                    where mn.ParentId == ParentIDValue && mn.ShowMenu == true && usr.UserId == userId
                    orderby mn.Opt
                    select mn).ToList();
            ID1 = ID1 == null ? string.Empty : ID1;
            ID2 = ID2 == null ? string.Empty : ID2;
            ID3 = ID3 == null ? string.Empty : ID3;
            
            DT.Where(x => x.MenuId.ToString() == ID1).ToList().ForEach(x => x.Descriptions = "active");
            DT.Where(x => x.MenuId.ToString() == ID2).ToList().ForEach(x => x.Descriptions = "active");
            DT.Where(x => x.MenuId.ToString() == ID3).ToList().ForEach(x => x.Descriptions = "active");
            
            foreach (Menu itemMenu in DT)
            {
                MenuController MeMC = new Controllers.MenuController();
                var Menu = new MenuViewModels
                {
                    Parent = itemMenu,
                    Child = MeMC.DTMenu(itemMenu.MenuId.ToString(), ID1, ID2, ID3)
                };

                DATA.Add(Menu);
            }

            return DATA;
        }

        public ActionResult Breadcrumb(string Controller, string Action)
        {
            List<Menu> DataMenu = new List<Menu>();
            var DMenu = _db.Menus.Where(x => x.Controller == Controller && x.Action == Action).FirstOrDefault();
            DataMenu.Add(DMenu);
            string ParentID = DMenu == null ? "" : DMenu.ParentId;
            var loop = false;
            while (loop == false)
            {
                var DRow = _db.Menus.Where(x => x.MenuId == ParentID).FirstOrDefault();
                if (DRow == null)
                {
                    loop = true;
                }
                else
                {
                    DataMenu.Add(DRow);
                    ParentID = DRow.ParentId;
                }
            }
            var Breadcrumb = DMenu == null ? null : DataMenu.OrderBy(x => x.Opt);
            return PartialView("_Breadcrumb", Breadcrumb);
        }
    }
}