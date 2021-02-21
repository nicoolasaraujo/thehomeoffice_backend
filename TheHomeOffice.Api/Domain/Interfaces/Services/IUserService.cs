using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Dtos;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User mappedUser);
        Task<AuthenticationResult> ValidateLogin(string email, string password);
        Task<User> GetUser(int id);
        Task DeleteUser(User user);
    }
}
