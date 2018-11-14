using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EbookShop.Services.Dtos;

namespace EbookShop.Services
{
    public interface IAuthenticationService
    {
        Task<string> Authenticate(CredentialsDTO credentialsDTO);
    }
}
