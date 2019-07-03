using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
        }
        protected void BaseButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
            switch (((Button)sender).Name)
            {
                case nameof(buttonSearch):
                    SearchButton_Click(sender, e);
                    break;
                case nameof(buttonClear):
                    ClearButton_Click(sender, e);
                    break;
                case nameof(buttonUpdate):
                    UpdateButton_Click(sender, e);
                    break;
                case nameof(buttonDelete):
                    DeleteButton_Click(sender, e);
                    break;
                case nameof(buttonEnd):
                default:
                    ExitButton_Click(sender, e);
                    break;
            }
        }

        public virtual void SearchButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void ClearButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void UpdateButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void DeleteButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
        }
        public virtual void ExitButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(MethodBase.GetCurrentMethod().Name);
            Close();
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視しています
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                //描画が完了したことを知らせる
                e.Handled = true;
            }
        }
    }
}
