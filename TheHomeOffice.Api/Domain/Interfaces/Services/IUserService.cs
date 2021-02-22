using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
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
        Task<Result<User>> CreateUser(User mappedUser);
        Task<Result<User>> ValidateLogin(string email, string password);
        Task<Result<User>> DeleteUser(int id);
        Task<Result<User>> UpdateUser(int id, User user);
        Task UpdatePlace(int userId, Address address);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(int userId);
    }
}
