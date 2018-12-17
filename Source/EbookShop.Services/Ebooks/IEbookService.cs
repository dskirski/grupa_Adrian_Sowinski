using EbookShop.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EbookShop.Services.Ebooks
{
    public interface IEbookService
    {
       Ebook[] GetEbooksByTextInAnyProperty(string text);
       Ebook[] GetEbooksByTitle(string title);
       Ebook[] GetEbooksByCategory(Category category);
       Ebook[] GetEbooksByAuthor(Author author);
    }
}
