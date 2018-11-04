using System.Threading.Tasks;
using EbookShop.Services.Dtos;

namespace EbookShop.Services
{
    public interface IRegistrationService
    {
        Task RegisterWithStandardEmailAsync(RegistrationDTO regDTO);

    }
}
