using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Domain.Dtos
{
    public class UserAdrressDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address UserAddress { get; set; }

    }
}
