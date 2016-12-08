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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseContext db = new DatabaseContext();
            //DatabaseContext db = null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var CatalogController = new CatalogController(db);
            Application.Run(CatalogController.RenderMainView());

            if (db != null) { db.Dispose(); }
        }
    }
}
