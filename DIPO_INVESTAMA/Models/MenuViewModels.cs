using DIPO_INVESTAMA.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIPO_INVESTAMA.Models
{
    public class MenuViewModels
    {
        public bool Checked { get; set; }
        public Menu Parent { get; set; }
        public List<MenuViewModels> Child { get; set; }
    }

    public class MenuModels
    {
        public List<MenuViewModels> Menu { get; set; }
    }
}