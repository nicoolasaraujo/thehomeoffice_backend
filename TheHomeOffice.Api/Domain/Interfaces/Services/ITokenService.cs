using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User model);
    }
}
