using Energetic.NET.Basic.Domain.Models;

namespace Energetic.NET.Basic.Domain.IResponsitories
{
    public interface IUserDomainRepository
    {
        Task<User?> FindByUserNameAsync(string userName);
        
        Task<User?> FindByEmailAdressAsync(string emailAdress);

        Task<User?> FindByPhoneNumberAsync(string phoneNumber);
    }
}
