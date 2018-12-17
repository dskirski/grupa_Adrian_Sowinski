using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EbookShop.DataAccess;
using EbookShop.Models;
using EbookShop.Services.Dtos;
using EbookShop.Services.Helpers;
using EbookShop.Services.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EbookShop.Services
{
    public class UserService : IUserService
    {
        private readonly ClaimsPrincipal _caller;
        private readonly EbookShopContext _appDbContext;
        private readonly IMapper _mapper;

        public UserService(EbookShopContext appDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
            _mapper = mapper;

        }


        public async Task<DashboardDTO> IsValidUserHTTP()
        {
            // user id claim
            var userId = _caller.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id);
            if (userId == null) throw new InvalidOperationException(ErrorMessages.IdClaimNotFound);

            var customer = await _appDbContext.Customers.Include(c => c.Identity)
                .SingleAsync(c => c.Identity.Id == userId.Value);
            if (customer == null) throw new InvalidOperationException(ErrorMessages.UserNotFound);

            var user = _mapper.Map<DashboardDTO>(customer.Identity);
            return user;
        }

        //Zwraca tablicę ebooków, w których tytule/nazwie kategorii/nazwie autora zawiera się dana fraza.
        public static Ebook[] GetEbooksByTextInAnyProperty(EbookShopContext context, string text)
        {
            List<Ebook> returnValue = new List<Ebook>();

            Ebook[] temp = GetEbooksByTitle(context, text);
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
        public static Ebook[] GetEbooksByTitle(EbookShopContext context, string title)
        {
            Ebook[] returnValue = context.Ebooks.Where(e => e.Title.Contains(title)).ToArray();

            return returnValue;
        }

        //Zwraca tablicę ebooków danej kategorii
        public static Ebook[] GetEbooksByCategory(EbookShopContext context, Category category)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainCategory(e, category)
            ).ToArray();

            return returnValue;
        }

        //Sprawdza czy ebook jest danej kategorii
        private static bool DoesEbookContainCategory(Ebook e, Category category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category == category;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w nazwie kategorii ebooka
        private static bool DoesEbookContainCategory(Ebook e, string category)
        {
            foreach (EbookCategories ebookCategories in e.EbookCategories)
                return ebookCategories.Category.CategoryName.Contains(category);

            return false;
        }

        //Zwraca tablicę ebooków z danym autorem
        public static Ebook[] GetEbooksByAuthor(EbookShopContext context, Author author)
        {
            Ebook[] returnValue = context.Ebooks.Where(
                (e) =>
                    DoesEbookContainAuthor(e, author)
            ).ToArray();

            return returnValue;
        }

        //Sprawdza czy dany autor jest autorem danego ebooka
        private static bool DoesEbookContainAuthor(Ebook e, Author author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author == author;

            return false;
        }

        //Sprawdza czy dana fraza zawiera się w imieniu bądź nazwisku autora
        private static bool DoesEbookContainAuthor(Ebook e, string author)
        {
            foreach (AuthorEbooks ebookCategories in e.AuthorEbooks)
                return ebookCategories.Author.FirstName.Contains(author) || ebookCategories.Author.LastName.Contains(author);

            return false;
        }
    }
}
