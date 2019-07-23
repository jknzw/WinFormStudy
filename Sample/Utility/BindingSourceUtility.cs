using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Sample
{
    public static class BindingSourceUtility
    {
        public static void SetFilter(this BindingSource bs, string colName, string filter)
        {
            bs.Filter = $"{colName} like '%{filter.Replace("'", "''")}%'";
        }
    }
}
