using Energetic.NET.Basic.Domain.IRepositories;

namespace Energetic.NET.Basic.Infrastructure.Responsitories
{
    internal class UserLoginHistoryRepository(BasicDbContext basicDbContext) :
        BaseRepository<UserLoginHistory>(basicDbContext), IUserLoginHistoryRepository
    {
    }
}
