using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NetTask6.Models;
using NetTask6.Repositories;

namespace NetTask6.Helpers
{
    class DefaultDataHelper
    {
        internal static async Task FillRepositories(
            IRepository<Movie> movieRepository,
            IRepository<Director> directorRepository,
            IRepository<Actor> actorRepository)
        { 
            var frank = new Director { DirectorId = 1, Name = "Фрэнк Дарабонт" };
            var nolan = new Director { DirectorId = 2, Name = "Кристофер Нолан" };
            var spilberg = new Director { DirectorId = 3, Name = "Стивен Спилберг" };
            var leone = new Director { DirectorId = 4, Name = "Серджио Леоне" };
            var fincher = new Director { DirectorId = 5, Name = "Дэвид Финчер" };
            var tkey = new Director { DirectorId = 6, Name = "Тони Кэй" };
            var besson = new Director { DirectorId = 7, Name = "Люк Бессон" };


            var robbins = new Actor { ActorId = 1, Name = "Тим Роббинс" };
            var freeman = new Actor { ActorId = 2, Name = "Морган Фриман" };
            var hanks = new Actor { ActorId = 3, Name = "Том Хэнкс" };
            var burns = new Actor { ActorId = 4, Name = "Эдвард Бёрнс" };
            var deniro = new Actor { ActorId = 5, Name = "Роберт Де Ниро" };
            var pitt = new Actor { ActorId = 6, Name = "Брэд Питт" };
            var norton = new Actor { ActorId = 7, Name = "Эдвард Нортон" };
            var reno = new Actor { ActorId = 8, Name = "Жан Рено" };
            var oldman = new Actor { ActorId = 9, Name = "Гари Олдман" };

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

            };

            for (var i = 0; i < movieData.Length; i++)
            {
                await movieRepository.Save(new Movie
                {
                    MovieId = i + 1,
                    Name = movieData[i].Name,
                    Year = movieData[i].Year,
                    Image = @"C:\Users\user\Documents\GitHub\spbu-homework\2016\DOTNET\NetTask6\NetTask6\DefaultPictures\" + 
                        movieData[i].Image,
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
