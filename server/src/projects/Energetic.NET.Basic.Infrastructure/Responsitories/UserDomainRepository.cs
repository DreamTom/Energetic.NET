using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class UserDomainRepository(BasicDbContext basicDbContext) 
        : BaseRepository<User>(basicDbContext), IUserDomainRepository
    {
        public Task<User?> FindByEmailAdressAsync(string emailAdress)
        {
            return basicDbContext.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAdress);
        }

        public Task<User?> FindByPhoneNumberAsync(string phoneNumber)
        {
            return basicDbContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public Task<User?> FindByUserNameAsync(string userName)
        {
            return basicDbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<User> RegisterByEmailAddressAsync(string emailAddress)
        {
            var user = new User(RegisterWay.EmailAddress, emailAddress);
            user.SetEmailAddress(emailAddress);
            _ = await basicDbContext.AddAsync(user);
            return user;
        }

        public async Task<User> RegisterByPhoneNumberAsync(string phoneNumber)
        {
            var user = new User(RegisterWay.PhoneNumber, phoneNumber);
            user.SetPhoneNumber(phoneNumber);
            _ = await basicDbContext.AddAsync(user);
            return user;
        }
    }
}
