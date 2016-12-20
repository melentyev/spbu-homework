using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetTask6.Services;

namespace NetTask6.Views
{
    public class Autocomplete : ComboBox
    {
        private IAutocompleteSource source;
        public Autocomplete()
        {
            var combobox = this;
            KeyPress += (sender, args) =>
            {
                if (source == null) { return; }
                var txt = combobox.Text;
                source.Suggest(Text.ToUpper()).ContinueWith(listTaks => {
                    var list = listTaks.Result;
                    combobox.Invoke(new Action(() => { 
                        if (list.Count() > 0) {
                            combobox.Items.Clear();
                            combobox.Items.AddRange(list);
                            var sText = combobox.Items[0].ToString();
                            combobox.SelectionStart = txt.Length;
                            combobox.SelectionLength = sText.Length - txt.Length;
                            combobox.DroppedDown = true;
                        }
                        else
                        {
                            combobox.DroppedDown = false;
                            combobox.SelectionStart = txt.Length;
                        }
                    }));
                });
            };
            KeyUp += (sender, e) =>
            {
                if (e.KeyCode == Keys.Back)
                {
                    int sStart = combobox.SelectionStart;
                    if (sStart > 0)
                    {
                        sStart--;
                        if (sStart == 0)
                        {
                            combobox.Text = "";
                        }
                        else
                        {
                            combobox.Text = combobox.Text.Substring(0, sStart);
                        }
                    }
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            };
        }
        public void SetSource(IAutocompleteSource src) { source = src; }
    }
}
