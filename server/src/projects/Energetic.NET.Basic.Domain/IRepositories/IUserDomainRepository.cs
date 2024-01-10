﻿
namespace Energetic.NET.Basic.Domain.IRepositories
{
    public interface IUserDomainRepository : IBaseRepository<User>
    {
        Task<User?> FindByUserNameAsync(string userName);

        Task<bool> IsExistsUserNameAsync(string userName, long id = 0);

        Task<User?> FindByEmailAdressAsync(string emailAdress);

        Task<User?> FindByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByPhoneNumberAsync(string phoneNumber);

        Task<User> RegisterByEmailAddressAsync(string emailAddress);

        Task<User> GetUserIncludeRolesAsync(long userId);

        Task<User> GetUserIncludeRolesAndResourcesAsync(long userId);

        Task<bool> IsEnableAsync(long userId);
    }
}
