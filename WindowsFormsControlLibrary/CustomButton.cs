using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsControlLibrary
{
    public partial class CustomButton : Button
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        private void CustomButton_Enter(object sender, EventArgs e)
        {
            BackColor = Color.LightGreen;
        }

        private void CustomButton_Leave(object sender, EventArgs e)
        {
            //BackColor = Color.Empty;
            BackColor = SystemColors.Control;            
        }
    }
}
