using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class UserDomainRepository(BasicDbContext basicDbContext)
        : BaseRepository<User>(basicDbContext), IUserDomainRepository
    {
        public Task<User?> FindByEmailAdressAsync(string emailAdress)
        {
            return GetQueryableSet().SingleOrDefaultAsync(u => u.EmailAddress == emailAdress);
        }

        public Task<User?> FindByPhoneNumberAsync(string phoneNumber)
        {
            return GetQueryableSet().SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }

        public Task<User?> FindByUserNameAsync(string userName)
        {
            return GetQueryableSet().SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<List<Resource>> GetUserResourcesAsync(long userId)
        {
            var user = await GetQueryableSet().Include(u => u.Roles)
                .ThenInclude(r => r.Resources.Where(a => a.IsEnable))
                .SingleAsync(u => u.Id == userId);
            return user.Roles.SelectMany(r => r.Resources).ToList();
        }

        public Task<User> GetUserIncludeRolesAsync(long userId)
        {
            return GetQueryableSet().Include(u => u.Roles)
                .SingleAsync(u => u.Id == userId);
        }

        public Task<bool> IsExistsUserNameAsync(string userName, long id = 0)
        {
            if (id > 0)
                return GetQueryableSet().AnyAsync(u => u.UserName == userName && u.Id != id);
            return GetQueryableSet().AnyAsync(u => u.UserName == userName);
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

        public Task<bool> IsEnableAsync(long userId)
        {
            return GetQueryableSet().AnyAsync(u => u.Id == userId && u.IsEnable);
        }

        public Task<bool> IsAdminAsync(long userId)
        {
            return GetQueryableSet().AnyAsync(u => u.Id == userId && u.IsAdmin);
        }

        public async Task<bool> IsHasResourceAsync(long userId, long resourceId)
        {
            var user = await GetQueryableSet().Include(u => u.Roles)
                .ThenInclude(r => r.Resources.Where(r => r.Id == resourceId))
                .SingleAsync(u => u.Id == userId);
            return user.Roles.SelectMany(r => r.Resources).Any();
        }
    }
}
