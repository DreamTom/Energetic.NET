using MediatR;

namespace Energetic.NET.Basic.Domain.Events
{
    public record UserLoginEvent(UserLoginHistory UserLoginHistory) : INotification;
}
