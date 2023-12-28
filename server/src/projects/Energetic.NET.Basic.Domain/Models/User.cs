using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Common;

namespace Energetic.NET.Basic.Domain.Models
{
    public class User(RegisterWay registerWay, string nickName) : BaseAuditableEntity, IAggregateRoot
    {
        public string? UserName { get; set; }

        public string NickName { get; init; } = nickName;

        public string? RealName { get; set; }

        private string? PasswordHash;

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public Gender Gender { get; set; }

        public Guid? AvatarId { get; set; }

        public bool IsAdmin { get; private set; }

        public RegisterWay RegisterWay { get; private set; } = registerWay;

        public List<Role> Roles { get; } = [];

        public void ChangePhone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void ChangeEmail(string emailAdress)
        {
            EmailAddress = emailAdress;
        }

        public void ChangePassword(string password)
        {
            PasswordHash = password.ComputeMd5Hash();
        }

        public bool CheckPassword(string password)
        {
            return PasswordHash == password.ComputeMd5Hash();
        }

        public void ChangeAvatar(Guid avatarId)
        {
            AvatarId = avatarId;
        }

        public void AddOrUpdate(string? realName, string? phoneNumber, string? emailAddress, Gender gender, Guid? avatarId)
        {
            RealName = realName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Gender = gender;
            AvatarId = avatarId;
        }

        public void AddOrUpdateRoles(List<Role> roles)
        {
            roles.ForEach(role =>
            {
                if(!Roles.Contains(role))
                    Roles.Add(role);
            });
        }
    }
}
