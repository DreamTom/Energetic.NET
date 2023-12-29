using Energetic.NET.Basic.Domain.IResponsitories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class UserDomainRepository(BasicDbContext basicDbContext) : IUserDomainRepository
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
    }
}
