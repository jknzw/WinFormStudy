using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.Utility
{
    public static class DataGridViewUtility
    {
        public static void ScrollToLast(this DataGridView dg)
        {
            dg.FirstDisplayedScrollingRowIndex = dg.Rows.GetLastRow(DataGridViewElementStates.None);
        }
    }
}
