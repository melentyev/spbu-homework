using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetTask6.Views
{
    public class YearInput: TextBox
    {
        const int min = 1895;
        public YearInput()
        {
            KeyPress += (sender, args) =>
            {
                args.Handled = !Char.IsDigit(args.KeyChar);
            };

            TextChanged += (sender, args) =>
            {
                
            };

            Validating += (sender, args) =>
            {

            };
        }
    }
}
