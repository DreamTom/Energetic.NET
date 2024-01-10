using Energetic.NET.Basic.Domain.Enums;
using Energetic.NET.Common;

namespace Energetic.NET.Basic.Domain.Models
{
    public class User(RegisterWay registerWay, string nickName) : BaseAuditableEntity, IAggregateRoot
    {
        public string? UserName { get; private set; }

        public string NickName { get; set; } = nickName;

        public string? RealName { get; private set; }

        private string? passwordHash;

        public string? PhoneNumber { get; private set; }

        public string? EmailAddress { get; private set; }

        public Gender Gender { get; private set; }

        public long? AvatarId { get; private set; }

        public bool IsAdmin { get; private set; }

        public RegisterWay RegisterWay { get; private set; } = registerWay;

        public List<Role> Roles { get; private set; } = [];

        public bool IsEnable { get; private set; }

        public void AddByUserName(string userName, string? realName, string password, Gender gender)
        {
            UserName = userName;
            Gender = gender;
            RealName = realName;
            IsEnable = true;
            SetPassword(password);
        }

        public void SetEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
            IsEnable = true;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            IsEnable = true;
        }

        public void AddOrUpdate(string userName, string? realName, Gender gender, long? avatarId, bool isEnable)
        {
            UserName = userName;
            RealName = realName;
            Gender = gender;
            AvatarId = avatarId;
            IsEnable = isEnable;
        }

        public void ChangeNickName(string nickName)
        {
            NickName = nickName;
        }

        public void SetRoles(List<Role> roles)
        {
            Roles = roles;
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
