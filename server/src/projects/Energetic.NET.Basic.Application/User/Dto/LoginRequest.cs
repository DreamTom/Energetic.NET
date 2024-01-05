﻿using Energetic.NET.Basic.Domain.Enums;

namespace Energetic.NET.Basic.Application.User.Dto
{
    public class LoginRequest
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? EmailAddress { get; set; }

        public string? PhoneNumber { get; set; }

        public string? SecondCode { get; set; }

        public RegisterWay LoginWay { get; set; }

        public string? VerificationCode { get; set; }
        
        public string? CaptchaId { get; set; }
    }
}
