using EbookShop.DataAccess;
using EbookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbookShop.Services.Ebooks
{
    public class EbookService 
    {
        private readonly EbookShopContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public EbookService(EbookShopContext context)
        {
            _context = context; 
        }
        /// <summary>
        ///Zwraca tablicę ebooków, w których tytule/nazwie kategorii/nazwie autora zawiera się dana fraza.
        /// </summary>
        public Ebook[] GetEbooksByTextInAnyProperty(string text)
        {
            List<Ebook> returnValue = new List<Ebook>();

            Ebook[] temp = GetEbooksByTitle(text);
            returnValue.AddRange(temp);

            temp = _context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, text)
            ).ToArray();
            returnValue.AddRange(temp);

            temp = _context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, text)
            ).ToArray();
            returnValue.AddRange(temp);

            return returnValue.ToArray();
        }
        /// <summary>
        ///Zwraca tablicę z ebookami, w których tytule zawiera się podana fraza
        /// </summary>
        public Ebook[] GetEbooksByTitle(string title)
        {
            Ebook[] returnValue = _context.Ebooks.Where(e => e.Title.Contains(title)).ToArray();

            return returnValue;
        }
        /// <summary>
        ///Zwraca tablicę ebooków danej kategorii
        /// </summary>
        public Ebook[] GetEbooksByCategory(Category category)
        {
            Ebook[] returnValue = _context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, category)
            ).ToArray();

            return returnValue;
        }
        /// <summary>
        ///Sprawdza czy ebook jest danej kategorii
        /// </summary>
        private bool DoesEbookContainCategory(Ebook e, Category category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category == category;

            return false;
        }
        /// <summary>
        ///Sprawdza czy dana fraza zawiera się w nazwie kategorii ebooka
        /// </summary>
        private bool DoesEbookContainCategory(Ebook e, string category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category.CategoryName.Contains(category);

            return false;
        }
        /// <summary>
        ///Zwraca tablicę ebooków z danym autorem
        /// </summary>
        public Ebook[] GetEbooksByAuthor(Author author)
        {
            Ebook[] returnValue = _context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, author)
            ).ToArray();

            return returnValue;
        }
        /// <summary>
        ///Sprawdza czy dany autor jest autorem danego ebooka
        /// </summary>
        private bool DoesEbookContainAuthor(Ebook e, Author author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author == author;

            return false;
        }
        /// <summary>
        ///Sprawdza czy dana fraza zawiera się w imieniu bądź nazwisku autora
        /// </summary>
        private bool DoesEbookContainAuthor(Ebook e, string author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author.FirstName.Contains(author) || ebookCategories.Author.LastName.Contains(author);

            return false;
        }
    }
}
