using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Common;

namespace Energetic.NET.Basic.Domain.Models
{
    public class User(RegisterWay registerWay, string nickName) : BaseAuditableEntity, IAggregateRoot
    {
        public string? UserName { get; private set; }

        public string NickName { get; init; } = nickName;

        public string? RealName { get; private set; }

        private string? passwordHash;

        public string? PhoneNumber { get; private set; }

        public string? EmailAddress { get; private set; }

        public Gender Gender { get; private set; }

        public long? AvatarId { get; private set; }

        public bool IsAdmin { get; private set; }

        public RegisterWay RegisterWay { get; private set; } = registerWay;

        public List<Role> Roles { get; } = [];

        public void AddByUserName(string userName, string? realName, string password, Gender gender)
        {
            UserName = userName;
            Gender = gender;
            RealName = realName;
            SetPassword(password);
        }

        public void SetEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }

        public void AddOrUpdate(string? realName, string? phoneNumber, string? emailAddress, Gender gender, long? avatarId)
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
                if (!Roles.Contains(role))
                    Roles.Add(role);
            });
        }

        public void SetPassword(string password)
        {
            passwordHash = password.ComputeMd5Hash();
        }

        public bool CheckPassword(string password)
        {
            return passwordHash == password.ComputeMd5Hash();
        }
    }
}
