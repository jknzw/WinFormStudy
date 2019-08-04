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
    public partial class CustomTextBox : TextBox
    {
        public CustomTextBox()
        {
            InitializeComponent();
        }

        private void CustomtextBox_Enter(object sender, EventArgs e)
        {
            BackColor = Color.LightGreen;
        }

        private void CustomtextBox_Leave(object sender, EventArgs e)
        {
            BackColor = SystemColors.Window;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
