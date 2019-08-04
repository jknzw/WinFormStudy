using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary
{
    public partial class CustomReadOnlyTextBox : CustomTextBox
    {
        public CustomReadOnlyTextBox()
        {
            InitializeComponent();

            // 編集を禁止する
            ReadOnly = true;

            // タブで移動できなくする
            TabStop = false;
        }
    }
}
