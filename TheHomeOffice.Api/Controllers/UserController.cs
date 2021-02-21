using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHomeOffice.Api.Domain.Dtos;
using TheHomeOffice.Api.Domain.Enumerators;
using TheHomeOffice.Api.Domain.Interfaces.Services;
using TheHomeOffice.Api.Domain.Models;

namespace TheHomeOffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(ITokenService tokenService, IUserService userService, IMapper mapper)
        {
            this.tokenService = tokenService;
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Createuser([FromBody] SignupUserDto newUser)
        {
            var mappedUser = this.mapper.Map<SignupUserDto, User>(newUser);
            var createdUser = await this.userService.CreateUser(mappedUser);
            var userMapped = this.mapper.Map<UserDto>(createdUser);
            return Created($"user/{userMapped.Id}", userMapped);

        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody] LoginDto userLogin)
        {
            var authResult = await this.userService.ValidateLogin(userLogin.Email, userLogin.Password);

            if (authResult.LoginResult == EnumLoginResult.FAIL)
            {
                return Unauthorized(new { authResult.Message });
            }

            var user = authResult.ReturnedUser;
            var userDto = this.mapper.Map<UserDto>(user);
            var token = this.tokenService.GenerateToken(user);

            userDto.Token = token;

            return Ok(userDto);
        }

        [HttpGet]
        [Authorize]
        public async Task<User> Getuser([FromBody] int id){
            User user =  new User();
            var getUser = await this.userService.GetUser(id);
            user = getUser;
            return user;
        }

        [HttpDelete]
        [Authorize]
        public async Task<Task<string>> Deleteuser([FromBody] int id){
            User user = new User();
            user = await Getuser(id);
            await this.userService.DeleteUser(user);
            return Task.FromResult("Usuario deletado com sucesso");
        }

    }
}
