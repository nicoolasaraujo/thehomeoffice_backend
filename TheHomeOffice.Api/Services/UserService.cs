using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Dtos;
using TheHomeOffice.Api.Domain.Interfaces.Services;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Services
{
    public class UserService : IUserService
    {
        private static List<User> users = new List<User>() { new User() {Id = 1, Email= "user@email.com", Password = "12345", Name = "string" } } ;
        public Task<User> CreateUser(User mappedUser)
        {
            mappedUser.Id = 0;
            users.Add(mappedUser);
            return Task.FromResult(mappedUser);
        }

        public Task<AuthenticationResult> ValidateLogin(string email, string password)
        {
            return Task.FromResult(
                new AuthenticationResult()
                {
                    LoginResult = Domain.Enumerators.EnumLoginResult.SUCCESS,
                    Message = "Login realizado com sucesso!",
                    ReturnedUser = users.Where(x => x.Email == email && x.Password == password).FirstOrDefault()
                });
        }
    }
}
