using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class DashboardViewModels
    {
        public string FilterReportBank { get; set; }
        public string PeriodeReportBank { get; set; }
        public string ViewByReportBank { get; set; }

        public string FilterReportAccount { get; set; }
        public string PeriodeReportAccount { get; set; }
        public string ViewByReportAccount { get; set; }

        public string FilterReportCashIn { get; set; }
        public string PeriodeReportCashIn { get; set; }
        public string ViewByReportCashIn { get; set; }

        public string FilterReportCashOut { get; set; }
        public string PeriodeReportCashOut { get; set; }
        public string ViewByReportCashOut { get; set; }
    }
}