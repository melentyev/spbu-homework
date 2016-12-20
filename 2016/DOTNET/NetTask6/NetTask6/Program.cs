using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetTask6.Models;
using NetTask6.Views;
using NetTask6.Controllers;

namespace NetTask6
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        internal static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Contains("--nodb"))
            {
                var CatalogController = new CatalogController();
                Application.Run(CatalogController.RenderMainView());
            }
            else
            {
                using (var db = new DatabaseContext())
                {
                    var CatalogController = new CatalogController(db);
                    Application.Run(CatalogController.RenderMainView());
                }
            }
        }
    }
}
