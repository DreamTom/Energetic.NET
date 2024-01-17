using Energetic.NET.Basic.Domain.Events;
using Energetic.NET.Basic.Domain.IRepositories;
using MediatR;

namespace Energetic.NET.Basic.Application.UserService.EventHandlers
{
    public class UserLoginEventHandler(IUserDomainRepository userDomainRepository) : INotificationHandler<UserLoginEvent>
    {
        public async Task Handle(UserLoginEvent notification, CancellationToken cancellationToken)
        {
            await userDomainRepository.AddUserLoginHistoryAsync(notification.UserLoginHistory);
        }
    }
}
