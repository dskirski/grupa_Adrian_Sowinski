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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Html;
using System.IO;
using Newtonsoft.Json.Serialization;

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


        public async Task<DashboardDto> IsValidUserHTTP()
        {
            // user id claim
            var userId = _caller.Claims.Single(c => c.Type == Constants.Strings.JwtClaimIdentifiers.Id);
            if (userId == null) throw new InvalidOperationException(ErrorMessages.IdClaimNotFound);

            var customer = await _appDbContext.Customers.Include(c => c.Identity)
                .SingleAsync(c => c.Identity.Id == userId.Value);
            if (customer == null) throw new InvalidOperationException(ErrorMessages.UserNotFound);

            var user = _mapper.Map<DashboardDto>(customer.Identity);
            return user;
        }
    }
}
