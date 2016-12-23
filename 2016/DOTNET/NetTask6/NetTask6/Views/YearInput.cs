using System;
using System.Windows.Forms;

using NetTask6.Helpers;
namespace NetTask6.Views
{
    public class YearInput: TextBox
    {
        private ErrorProvider errorProvider;
        internal bool IsValid
        {
            get
            {
                int parsed;
                if (Int32.TryParse(Text, out parsed))
                {
                    return ValidationHelper.ValidateYear(parsed);
                }
                return false;
            }
        }
        internal bool IsNumber
        {
            get
            { 
                int parsed;
                return Int32.TryParse(Text, out parsed);
            }
        }

        public YearInput()
        {
            KeyPress += (sender, args) =>
            {
                args.Handled = !Char.IsDigit(args.KeyChar) && args.KeyChar != '\b';
            };

            Validating += (sender, args) =>
            {
                errorProvider.SetError(this, 
                    IsValid ? "" : String.Format(Properties.Resources.YearInputInvalid, ValidationHelper.LumiereFirstFilmDate));
            };
        }
        internal void SetErrorProvider(ErrorProvider ep) { errorProvider = ep; }
    }
}
