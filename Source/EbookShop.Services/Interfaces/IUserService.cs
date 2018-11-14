using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EbookShop.Services.Dtos;
namespace EbookShop.Services
{
    public interface IUserService
    {
        Task<DashboardDTO> IsValidUserHTTP();
    }
}
