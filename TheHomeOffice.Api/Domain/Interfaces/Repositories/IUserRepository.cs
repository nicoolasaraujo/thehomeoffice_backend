using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        public IEnumerable<User> FindAll();
        Task<IEnumerable<User>> FindByCondition(Func<User, bool> predicate);
    }
}
