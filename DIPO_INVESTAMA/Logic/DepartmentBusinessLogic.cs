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
    public class DepartmentBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static DepartmentBusinessLogic deptBusinessLogic = null;
        public static DepartmentBusinessLogic getInstance()
        {
            if (deptBusinessLogic == null)
            {
                deptBusinessLogic = new DepartmentBusinessLogic();
                return deptBusinessLogic;
            }
            else
            {
                return deptBusinessLogic;
            }
        }

        public PagedList<sp_DepartmentSelect_Result> ListDepartment()
        {
            var page = new PagedList<sp_DepartmentSelect_Result>();
            page.Content = _db.sp_DepartmentSelect().ToList();
            return page;
        }
        public int CreateDepartment(sp_DepartmentSelect_Result model)
        {
            return _db.sp_DepartmentCreate(model.DepartmentName,SessionManager.userId());
        }
        public int UpdateDepartment(sp_DepartmentSelect_Result model)
        {
            return _db.sp_DepartmentUpdate(model.DepartmentId, model.DepartmentName, SessionManager.userId());
        }

        public Department getDepartmentById(string id)
        {
            return _db.Departments.Where(b => b.DepartmentId.Equals(id)).FirstOrDefault();
        }

        public int DeleteDepartment(string id)
        {
            return _db.sp_DepartmentDelete(id);
        }

        public DataTable getDepartmentDDL()
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("NAME", typeof(string));
            foreach (var item in _db.sp_DepartmentDDL())
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