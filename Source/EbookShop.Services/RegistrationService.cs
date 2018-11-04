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
        /// <param name="regDTO">Registration data transfer object</param>
        
        public async Task RegisterWithStandardEmailAsync(RegistrationDTO regDTO)
        {
            // map dto to AppUser class
            var user = _mapper.Map<AppUser>(regDTO);

            // Try to create the new user
            var result = await _userManager.CreateAsync(user, regDTO.Password);
            // If it fails throw an exception
            if (!result.Succeeded)
                throw new InvalidOperationException(result.ErrorMessage());
                
            // Add new user to customers table with the identity id assigned. 
            await _context.Customers.AddAsync(new Customer { IdentityId = user.Id });
            // Commit changes to database
            await _context.SaveChangesAsync();
        
        }
    }
}
