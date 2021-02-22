using Ardalis.Result;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Interfaces.Repositories;
using TheHomeOffice.Api.Domain.Interfaces.Services;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Services
{
    public class UserService : IUserService
    {
        private IRepositoryBase<User> userRepository;

        public UserService(IRepositoryBase<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result<User>> CreateUser(User mappedUser)
        {
            bool userExists = await this.userRepository.Exists(u => u.Email == mappedUser.Email);

            if (userExists)
            {
                return Result<User>.Invalid(new List<ValidationError>() { new ValidationError() { ErrorMessage = "Usuário já está cadastrado no banco de dados!", Identifier = "", Severity = ValidationSeverity.Error } });
            }

            User createdUser = await this.userRepository.AddAsync(mappedUser);

            return Result<User>.Success(createdUser);
        }

        public async Task<Result<User>> ValidateLogin(string email, string password)
        {
            var users = await this.userRepository.GetByCondition(user => user.Email == email && user.Password == password);
            var user = users.FirstOrDefault();
            if (user == null)
            {
                return Result<User>.Invalid(new List<ValidationError>() { new ValidationError() { ErrorMessage = "Usuário ou senha incorreto!" }  });
            }

            return Result<User>.Success(user, "Logado, vai filhão!");    
        }

        public async Task<Result<User>> DeleteUser(int id)
        {
            var userToDelete = await this.userRepository.GetByCondition(x => x.Id == id);
            if(userToDelete?.Count() <= 0)
            {
                return Result<User>.NotFound();
            }
            var deletedUser = userToDelete.First<User>();
            await this.userRepository.DeleteAsync(deletedUser);

            return Result<User>.Success(deletedUser);
        }

        public async Task<Result<User>> UpdateUser(int id, User user)
        {
            var userToUpdate = await this.userRepository.GetByCondition(x => x.Id == id);
            if (userToUpdate?.Count() <= 0)
            {
                return Result<User>.NotFound();
            }

            var updatedUser = userToUpdate.First<User>();
            await this.userRepository.UpdateAsync(updatedUser);

            return Result<User>.Success(updatedUser);
        }

        public async Task UpdatePlace(int userId, Address address)
        {
            var userToUpdate = await this.userRepository.GetByCondition(x => x.Id == userId);
            var updateData = userToUpdate.FirstOrDefault();
            updateData.UserAddress = address;
            await this.userRepository.UpdateAsync(updateData);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.userRepository.GetByCondition(u => !u.IsAdmin);
        }

        public async Task<User> GetUserById(int userId)
        {
            var users = await this.userRepository.GetByCondition(u => u.Id == userId);
            return users.FirstOrDefault();
        }
    }
}
