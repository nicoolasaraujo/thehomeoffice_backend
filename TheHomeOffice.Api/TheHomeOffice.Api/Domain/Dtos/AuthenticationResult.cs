
using System;
using System.Collections.Generic;
using System.Text;
using TheHomeOffice.Api.Domain.Enumerators;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Domain.Dtos
{
    public class AuthenticationResult
    {
        public EnumLoginResult LoginResult { get; set; }

        public string Message { get; set; }

        public User ReturnedUser { get; set; }
    }
}
