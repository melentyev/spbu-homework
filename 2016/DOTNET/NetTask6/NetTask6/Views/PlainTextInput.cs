using System;
using System.Windows.Forms;

using NetTask6.Helpers;
namespace NetTask6.Views
{
    public class PlainTextInput : TextBox
    {
        private ErrorProvider errorProvider;
        internal bool IsValid
        {
            get
            {
                return ValidationHelper.ValidatePlainText(Text);
            }
        }

        public PlainTextInput()
        {

            Validating += (sender, args) =>
            {
                errorProvider.SetError(this,
                    IsValid ? "" : Properties.Resources.PlainTextInputInvalid);
            };
        }
        internal void SetErrorProvider(ErrorProvider ep) { errorProvider = ep; }
    }
}
