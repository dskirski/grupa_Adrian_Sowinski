using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EbookShop.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace EbookShop.DataAccess
{
    public class EbookShopInitializer
    {
        public string EbookImagesDirectory { get; set; }
        public string EbooksDirectory { get; set; }

        public static void Initialize(EbookShopContext context,
            IHostingEnvironment hostingEnvironment)
        {
            var root = Path.GetFullPath(@"../", hostingEnvironment.ContentRootPath);
            var imgStorage = Path.Combine(root, "Resources\\FileStorage\\img");
            var exists = Directory.Exists(imgStorage);
            if (!exists) Debug.Fail($"Directory {imgStorage} does not exists");
            var initializer = new EbookShopInitializer();
            Initialize(context, imgStorage, null);


        }

        public static void Initialize(EbookShopContext context,
            string ebookImagesDirectory,
            string ebooksDirectory
            )
        {
            var initializer = new EbookShopInitializer();
            initializer.EbookImagesDirectory = ebookImagesDirectory;
            initializer.EbooksDirectory = ebooksDirectory;
            initializer.SeedEverythink(context);
        }

        private void SeedEverythink(EbookShopContext context)
        {
           // context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            if (context.Ebooks.Any())
            {
                return;
            }
           
            SeedGenres(context);
            SeedAuthors(context);
            SeedEbooks(context);
            SeedEbookGenres(context);
            SeedEbookAuthors(context);
            SeedFiles(context); 
            context.SaveChanges();
        }
        private void SeedGenres(EbookShopContext context)
        {
            var genres = new Genre[]
            {
                    new Genre()
                    {
                        Name = "Kryminał",
                    },
                    new Genre()
                    {
                        Name = "Sensacja"
                    },
                    new Genre()
                    {
                        Name = "Thriller"
                    },
                    new Genre()
                    {
                        Name = "Fantastyka"
                    },
                    new Genre()
                    {
                        Name = "Horror"
                    }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();
        }
        private void SeedAuthors(EbookShopContext context)
        {
            var authors = new Author[] {
              new Author()
              {
                FirstName = "Lee",
                LastName = "Child"
               },
              new Author()
              {
                  FirstName = "Rebecca",
                  LastName = "Fleet"
              },
              new Author()
              {
                  FirstName = "Dmitry", 
                  LastName = "Glukhovsky"
              },
              new Author()
              {
                  FirstName = "Bill",
                  LastName = "Clinton"
              },
              new Author()
              {
                  FirstName = "James",
                  LastName = "Patterson"
              },
              new Author()
              {
                  FirstName = "Stephen",
                  LastName = "King"
              }

            };
            context.AddRange(authors);
            context.SaveChanges(); 
        }

        private void SeedEbooks(EbookShopContext context)
        {
            var ebooks = new Ebook[]
             {
                 new Ebook(){
                Title = "Czas przeszły",
                ISBN = "0912930281347",
                Description = "Reacher postanowił podążyć szlakiem zachodzącego słońca i przejechać z Maine do Kalifornii. Ale, jak zwykle, nie ujechał daleko. Przy leśnej drodze w Nowej Anglii widzi drogowskaz do miejsca...",
                Price = 29.99m
                },
                 new Ebook()
                 {
                     Title = "Zamiana",
                     ISBN = "1912930211347",
                     Description = "Uważaj, kogo wpuszczasz do domu...To miał być dla nich wyjątkowy czas: z dala od codziennej rutyny, dziecka i zmartwień.Nowy początek dla ich pogrążonego w kryzysie związku – jego chorobliwych reakcji i jej niewierności. Caroline i Francis zamienili się mieszkaniami: na tydzień oddali swoje mieszkanie w mieście i przenieśli się do eleganckiego domu na eleganckim przedmieściu Londynu.Mieli zapomnieć o problemach, odnaleźć dawny żar, który ich połączył.",
                     Price = 23.36m
                 },
                 new Ebook()
                 {
                     Title = "Uniwersum Metro 2033. Metro 2033",
                     ISBN = "3912930211341",
                     Description = "Ten thriller SF, którego wartka akcja i niezwykle sugestywnie przedstawiony świat nie pozwolą ci się od niego oderwać aż do ostatniej strony, to nie tylko wspaniała rozrywka i uczta dla czytelnika. To także portret człowieka u schyłku cywilizacji; przejmująca analiza jego niezmiennej natury. To obraz świata po końcu świata.",
                     Price = 24.64m
                 },
                 new Ebook()
                 {
                     Title = "Gdzie jest prezydent",
                     ISBN = "9952910211347",
                     Description = "To się za chwilę wydarzy. Najgroźniejszy atak terrorystyczny w historii rozpocznie się za 6… 5… 4… Zniknie wszystko. Dostęp do wody, prądu, twoich oszczędności. Nie zadzwonisz do rodziny. Nie będziesz wiedział, co się dzieje. Nie spodziewałeś się, że to może dotyczyć także ciebie. Ktoś tylko czeka, by zrealizować swój plan. Ma sprzymierzeńca w Białym Domu. Wystarczy, że wpisze hasło.",
                     Price = 32.49m
                 },
                 new Ebook()
                 {
                     Title = "Outsider",
                     ISBN = "5956911211347",
                     Description = "W parku miejskim znalezione zostaje zmasakrowane ciało jedenastoletniego chłopca. Naoczni świadkowie i odciski palców nie pozostawiają wątpliwości: sprawcą zbrodni jest jeden z najbardziej lubianych obywateli Flint City. To Terry Maitland, trener drużyn młodzieżowych, nauczyciel angielskiego, mąż i ojciec dwóch córek. Detektyw Ralph Anderson, którego syna Maitland kiedyś trenował, nakazuje przeprowadzić natychmiastowe aresztowanie w świetle jupiterów. Maitland ma wprawdzie alibi, ale Anderson i prokurator okręgowy wkrótce zdobywają kolejny niezbity dowód: ślady DNA. Sprawa wydaje się oczywista.",
                     Price = 31.50m
                 }
             };

            context.Ebooks.AddRange(ebooks);
            context.SaveChanges(); 
        }
        private void SeedEbookGenres(EbookShopContext context)
        {
            var genres = context.Genres.ToList();

            var ebookGenres = new EbookGenre[]
            {
                  new EbookGenre()
                        {
                            EbookId = 1,
                            GenreId = 1
                        },
                        new EbookGenre()
                        {
                            EbookId = 1,
                            GenreId = 2
                        },
                        new EbookGenre()
                        {
                             EbookId = 1,
                             GenreId = 3
                        },
                       new EbookGenre()
                        {
                           EbookId = 2,
                           GenreId = 1
                        },
                        new EbookGenre()
                        {
                             EbookId = 2,
                             GenreId = 2
                        },
                        new EbookGenre()
                        {
                            EbookId = 2,
                            GenreId = 3
                        },
                        new EbookGenre()
                        {
                            EbookId = 3,
                            GenreId = 4
                        },
                        new EbookGenre()
                        {
                             EbookId = 3,
                             GenreId = 5
                        },
                        new EbookGenre()
                        {
                            EbookId = 4,
                            GenreId = 1 
                        },
                        new EbookGenre()
                        {
                            EbookId = 4,
                            GenreId = 2 
                        },
                        new EbookGenre()
                        {
                            EbookId = 4,
                            GenreId = 3
                        },
                        new EbookGenre()
                        {
                            EbookId = 5,
                            GenreId = 5
                        },
                        new EbookGenre()
                        {
                            EbookId = 5,
                            GenreId = 4
                        }

            };
            context.EbookGenres.AddRange(ebookGenres);
            context.SaveChanges(); 
        }
        private void SeedEbookAuthors(EbookShopContext context)
        {
            var ebookAuthors = new EbookAuthor[]
            {
                 new EbookAuthor()
                        {
                            AuthorId = 1,
                            EbookId = 1
                        },
                 new EbookAuthor()
                 {
                     AuthorId = 2,
                     EbookId = 2
                 },
                 new EbookAuthor()
                 {
                     AuthorId = 3,
                     EbookId = 3
                 },
                 new EbookAuthor()
                 {
                     AuthorId = 4,
                     EbookId = 4,
                 },
                 new EbookAuthor()
                 {
                     AuthorId = 5,
                     EbookId = 4
                 },
                 new EbookAuthor()
                 {
                     AuthorId = 6,
                     EbookId = 5
                 }
            };
            context.EbookAuthors.AddRange(ebookAuthors);
            context.SaveChanges();
        }

        private void SeedFiles(EbookShopContext context)
        {
            var files = new FilePath[]
            {
                 new FilePath()
                       {
                           FileName = "czas-przeszly.jpg",
                           FileType = FileType.EBOOK_COVER,
                           Path = Path.Combine(this.EbookImagesDirectory, "czas-przeszly.jpg"),
                           EbookId = 1
                       },
                 new FilePath()
                 {
                     FileName = "zamiana.jpg",
                     FileType = FileType.EBOOK_COVER,
                     Path = Path.Combine(this.EbookImagesDirectory,"zamiana.jpg"),
                     EbookId = 2
                 },
                 new FilePath()
                 {
                     FileName = "uniwersum-metro-2033.jpg",
                     FileType = FileType.EBOOK_COVER,
                     Path = Path.Combine(this.EbookImagesDirectory,"uniwersum-metro-2033.jpg"),
                     EbookId = 3
                 },
                 new FilePath()
                 {
                     FileName = "gdzie-jest-prezydent.jpg",
                     FileType = FileType.EBOOK_COVER,
                     Path = Path.Combine(this.EbookImagesDirectory, "gdzie-jest-prezydent.jpg"),
                     EbookId = 4
                 },
                 new FilePath()
                 {
                     FileName = "outsider.jpg",
                     FileType = FileType.EBOOK_COVER,
                     Path = Path.Combine(this.EbookImagesDirectory,"outsider.jpg"),
                     EbookId = 5
                 }
            };
            context.Files.AddRange(files);
            context.SaveChanges(); 
        }


    }
}
