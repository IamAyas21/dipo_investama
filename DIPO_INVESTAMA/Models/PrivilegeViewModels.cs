using DIPO_INVESTAMA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class PrivilegeViewModels
    {
        public Role Parent { get; set; }
        public List<MenuViewModels> Menu { get; set; }
    }
}