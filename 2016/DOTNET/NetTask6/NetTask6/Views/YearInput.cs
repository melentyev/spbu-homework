using System;
using System.Windows.Forms;

namespace NetTask6.Views
{
    public class YearInput: TextBox
    {
        const int LumiereFirstFilmDate = 1895;

        private ErrorProvider errorProvider;
        internal bool IsValid
        {
            get
            {
                int parsed;
                if (Int32.TryParse(Text, out parsed))
                {
                    if (parsed >= LumiereFirstFilmDate && parsed <= DateTime.Now.Year)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public YearInput()
        {
            KeyPress += (sender, args) =>
            {
                args.Handled = !Char.IsDigit(args.KeyChar);
            };

            Validating += (sender, args) =>
            {
                errorProvider.SetError(this, 
                    IsValid ? "" : String.Format(Properties.Resources.YearInputInvalid, LumiereFirstFilmDate));
            };
        }
        internal void SetErrorProvider(ErrorProvider ep) { errorProvider = ep; }
    }
}
