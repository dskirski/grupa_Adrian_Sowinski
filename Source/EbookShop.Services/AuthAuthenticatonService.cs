using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EbookShop.Models;
using EbookShop.Services.Dtos;
using EbookShop.Services.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace EbookShop.Services
{
    public class AuthAuthenticationService : IAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthAuthenticationService(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        /// <summary>
        /// Authenticates user and returns jwt token. 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<string> Authenticate(CredentialsDTO credentials)
        {
            //Validate credentials and generate user identity
            ClaimsIdentity identity = null; 
            try
            {
                identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            }// rethrow exception
            catch (InvalidOperationException) { throw; }

            return await Helpers.Tokens.GenerateJwt(identity, _jwtFactory, credentials.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
            
        }

        /// <summary>
        /// Checks user credentials and generates him claims identity. 
        /// </summary>
        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new InvalidOperationException(ErrorMessages.UsernameOrPasswordIncorrect);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName) ?? throw new InvalidOperationException(ErrorMessages.UsernameOrPasswordIncorrect);
            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }// Credentials are invalid, or account doesn't exist
            else throw new InvalidOperationException(ErrorMessages.UsernameOrPasswordIncorrect);
            
        }
    }
}
