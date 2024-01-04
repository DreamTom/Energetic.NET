using Energetic.NET.Basic.Domain.Models;

namespace Energetic.NET.Basic.Domain.IResponsitories
{
    public interface IUserDomainRepository
    {
        Task<User?> FindByUserNameAsync(string userName);
        
        Task<User?> FindByEmailAdressAsync(string emailAdress);

        Task<User?> FindByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByEmailAddressAsync(string emailAddress);
    }
}
