using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample
{
    public static class FormUtility
    {
        public static Dictionary<string, dynamic> GetControlDictionary(this Form form)
        {
            IEnumerable<(string, dynamic)> list = GetControlValue(form);

            // キーが重複する KeyValuePair を削除する
            IEnumerable<(string key, dynamic value)> pairs = list.GroupBy(
                ((string key, dynamic value) pair) => pair.key,
                (string key, IEnumerable<(string, dynamic)> keyValues) => keyValues.First());

            // KeyValuePair のシーケンスを Dictionary 化する
            return pairs.ToDictionary(
              ((string key, dynamic value) pair) => pair.key,
              ((string key, dynamic value) pair) => pair.value);
        }

        public static IEnumerable<(string, dynamic)> GetControlValue(this Form form)
        {
            return GetControlValue(form.Controls);
        }

        private static IEnumerable<(string, dynamic)> GetControlValue(this Control.ControlCollection Controls)
        {
            IEnumerable<(string, dynamic)> list = new List<(string, dynamic)>(); ;
            foreach (Control ctrl in Controls)
            {
                switch (ctrl)
                {
                    case Button button:
                        list = list.Append((button.Name, button.Text));
                        break;
                    case TextBox textBox:
                        list = list.Append((textBox.Name, textBox.Text));
                        break;
                    case Label label:
                        list = list.Append((label.Name, label.Text));
                        break;
                    case ComboBox combo:
                        list = list.Append((combo.Name, combo.Text));
                        break;
                    case DateTimePicker dateTimePicker:
                        list = list.Append((dateTimePicker.Name,dateTimePicker.Value));
                        break;
                    default:
                        list = list.Concat(GetControlValue(ctrl.Controls));
                        break;
                }
            }
            return list;
        }
    }
}
