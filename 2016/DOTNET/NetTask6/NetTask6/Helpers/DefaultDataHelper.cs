using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetTask6.Models;
using NetTask6.Repositories;

namespace NetTask6.Helpers
{
    internal sealed class DefaultDataHelper
    {
        internal static async Task FillRepositories(
            IRepository<Movie> movieRepository,
            IRepository<Director> directorRepository,
            IRepository<Actor> actorRepository)
        { 
            var frank = new Director { Name = "Фрэнк Дарабонт" };
            var nolan = new Director { Name = "Кристофер Нолан" };
            var spilberg = new Director { Name = "Стивен Спилберг" };
            var leone = new Director { Name = "Серджио Леоне" };
            var fincher = new Director { Name = "Дэвид Финчер" };
            var tkey = new Director { Name = "Тони Кэй" };
            var besson = new Director { Name = "Люк Бессон" };

            var robbins = new Actor { Name = "Тим Роббинс" };
            var freeman = new Actor { Name = "Морган Фриман" };
            var hanks = new Actor { Name = "Том Хэнкс" };
            var burns = new Actor { Name = "Эдвард Бёрнс" };
            var deniro = new Actor { Name = "Роберт Де Ниро" };
            var pitt = new Actor { Name = "Брэд Питт" };
            var norton = new Actor { Name = "Эдвард Нортон" };
            var reno = new Actor { Name = "Жан Рено" };
            var oldman = new Actor { Name = "Гари Олдман" };

            var movieData = new[] {
                new { Name = "Побег из Шоушенка", Year = 1994u, Country = "США", Image = "326.jpg",
                    Director = frank, Actors = new Actor[] { robbins, freeman } },

                new { Name = "Бэтмен: Начало", Year = 2005u, Country = "США",  Image = "47237.jpg",
                    Director = nolan, Actors = new Actor[] { freeman } },

                new { Name = "Зеленая миля", Year = 1999u, Country = "США", Image = "435.jpg",
                    Director = frank, Actors = new Actor[] { hanks }},

                new { Name = "Спасти рядового Райана", Year = 1998u, Country = "США", Image = "371.jpg",
                    Director = spilberg, Actors = new Actor[] { hanks, burns } },

                new { Name = "Однажды в Америке", Year = 1983u, Country = "Италия", Image = "469.jpg",
                    Director = leone, Actors = new Actor[] { deniro } },

                new { Name ="Семь", Year = 1995u, Country = "США", Image="377.jpg",
                    Director = fincher, Actors = new Actor[] { freeman, pitt } },

                new { Name ="Бойцовский клуб", Year = 1999u, Country = "США", Image="361.jpg",
                    Director = fincher, Actors = new Actor[] { pitt, norton } },

                new { Name ="Американская история Х", Year = 1998u, Country = "США", Image="382.jpg",
                    Director = tkey, Actors = new Actor[] { norton } },

                new { Name ="Леон", Year = 1994u, Country = "Франция", Image="389.jpg",
                    Director = besson, Actors = new Actor[] { reno, oldman } },

                new { Name ="Пятый элемент", Year = 1997u, Country = "Франция", Image="5elem.jpg",
                    Director = besson, Actors = new Actor[] { oldman } },
            };

            var imagesBaseDir = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\DefaultPictures\"));
            for (var i = 0; i < movieData.Length; i++)
            {
                await movieRepository.Save(new Movie
                {
                    MovieId = i + 1,
                    Name = movieData[i].Name,
                    Year = (int)movieData[i].Year,
                    Country = movieData[i].Country,
                    Image = imagesBaseDir + movieData[i].Image,
                    Director = movieData[i].Director,
                    Actors = movieData[i].Actors,
                    
                });
            }

            await directorRepository.Save(frank);
            await directorRepository.Save(nolan);
            await directorRepository.Save(spilberg);

            await actorRepository.Save(robbins);
            await actorRepository.Save(freeman);
            await actorRepository.Save(hanks);
            await actorRepository.Save(burns);
        }
    }
}
