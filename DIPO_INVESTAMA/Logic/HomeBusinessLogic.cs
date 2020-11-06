using DIPO_INVESTAMA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Logic
{
    public class HomeBusinessLogic
    {
        DIPO_INVESTAMAEntities _db = new DIPO_INVESTAMAEntities();
        static HomeBusinessLogic homeLogic = null;
        public static HomeBusinessLogic getInstance()
        {
            if (homeLogic == null)
            {
                homeLogic = new HomeBusinessLogic();
                return homeLogic;
            }
            else
            {
                return homeLogic;
            }
        }

        public List<sp_HomeTileReport_Result> GetHomeTile()
        {
            var list = _db.sp_HomeTileReport().ToList();
            return list;
        }
    }
}