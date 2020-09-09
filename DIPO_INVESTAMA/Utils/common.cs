using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Utils
{
    public class common
    {
        [NonAction]
        public static SelectList ToSelectList(DataTable table, string valueField, string textField, string selectedValue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text", selectedValue);
        }
    }
}