using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EbookShop.DataAccess;
using EbookShop.Models;
using EbookShop.Services.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EbookShop.Services.Helpers;
using System.Security.Claims;

namespace EbookShop.Services
{
  
    public class RegistrationService : IRegistrationService
    {

        private readonly IMapper _mapper;
        private readonly EbookShopContext _context;
        private readonly UserManager<AppUser> _userManager;
 
        public RegistrationService(IMapper mapper, UserManager<AppUser> userManager, EbookShopContext context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registrationDTO">Registration data transfer object</param>
        /// <returns>Created user id.</returns>

        public async Task<string> RegisterAsync(RegistrationDTO registrationDTO)
        {
            // map dto to AppUser class
            var user = _mapper.Map<AppUser>(registrationDTO);

            // Try to create the new user
            var result = await _userManager.CreateAsync(user, registrationDTO.Password);
            // If it fails throw an exception
            if (!result.Succeeded)
                throw new InvalidOperationException(result.ErrorMessage());
            var customer = new Customer { IdentityId = user.Id };

            // Add new user to customers table with the identity id assigned. 
            await _context.Customers.AddAsync(customer);
            // Commit changes to database
            await _context.SaveChangesAsync();
            return customer.IdentityId;
            
        }

    }
}
