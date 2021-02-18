using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Dtos;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Mapper
{
    public class UserMapper : Profile 
    {
        public UserMapper() {
            #region Dto to Domain
            CreateMap<SignupUserDto, User>();

            #endregion Dto to Domain

            #region Domain to Dto

            CreateMap<User, UserDto>();
            #endregion Domain to Dto
        }

    }
}
