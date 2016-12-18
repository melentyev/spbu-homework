using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EfTest
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }

    public class Movie
    {
        public int MovieId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var db = new DatabaseContext())
            {
                db.Movies.Add(new Movie { Url = "http://blogs.msdn.com/adonet" });
                var count = db.SaveChanges();
                Console.WriteLine("{0} records saved to database", count);

                Console.WriteLine();
                Console.WriteLine("All blogs in database:");
                foreach (var blog in db.Movies)
                {
                    Console.WriteLine(" - {0}", blog.Url);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
