using EbookShop.DataAccess;
using EbookShop.Models;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EbookShop.Services
{
    public class SearchingService
    {
        private EbookShopContext context;

        public SearchingService(EbookShopContext context)
        {
            this.context = context;
        }

        //Zwraca tablicę ebooków, w których tytule/nazwie kategorii/nazwie autora zawiera się dana fraza.
        public Ebook[] SearchEbooksByAnyProperty(string text)
        {
            List<Ebook> returnValue = new List<Ebook>();

            Ebook[] temp = SearchEbooksByTitle(text);
            returnValue.AddRange(temp);

            temp = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, text)
            ).ToArray();
            returnValue.AddRange(temp);

            temp = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, text)
            ).ToArray();
            returnValue.AddRange(temp);


            return returnValue.ToArray();
        }

        //Zwraca tablicę z ebookami, w których tytule zawiera się podana fraza
        public Ebook[] SearchEbooksByTitle(string title)
        {
            Ebook[] returnValue = context.Ebooks.Where(e => e.Title.Contains(title)).ToArray();

            return returnValue;
        }

        //Zwraca tablicę ebooków danej kategorii
        public Ebook[] SearchEbooksByCategory(Genre category)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, category)
            ).ToArray();

            return returnValue;
        }

        //Sprawdza czy ebook jest danej kategorii
        private bool DoesEbookContainCategory(Ebook e, Genre category)
        {
            foreach (EbookGenre ebookCategories in e.EbookGenres)
                return ebookCategories.Genre == category;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w nazwie kategorii ebooka
        private bool DoesEbookContainCategory(Ebook e, string category)
        {
            foreach (EbookGenre ebookCategories in e.EbookGenres)
                return ebookCategories.Genre.Name.Contains(category);

            return false;
        }

        //Zwraca tablicę ebooków z danym autorem
        public Ebook[] SearchEbooksByAuthor(Author author)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, author)
            ).ToArray();

            return returnValue;
        }

        //Sprawdza czy dany autor jest autorem danego ebooka
        private bool DoesEbookContainAuthor(Ebook e, Author author)
        {
            foreach (EbookAuthor ebookCategories in e.EbookAuthors)
                return ebookCategories.Author == author;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w imieniu bądź nazwisku autora
        private bool DoesEbookContainAuthor(Ebook e, string author)
        {
            foreach (EbookAuthor ebookCategories in e.EbookAuthors)
                return ebookCategories.Author.FirstName.Contains(author) || ebookCategories.Author.LastName.Contains(author);

            return false;
        }
    }
}
