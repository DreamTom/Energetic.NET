
namespace Energetic.NET.Basic.Domain.IRepositories
{
    public interface IUserDomainRepository : IBaseRepository<User>
    {
        Task<User?> FindByUserNameAsync(string userName);

        Task<User?> FindByEmailAdressAsync(string emailAdress);

        Task<User?> FindByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByEmailAddressAsync(string emailAddress);
    }
}
