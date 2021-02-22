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
        public async Task<ActionResult> Createuser([FromBody] SaveUser newUser)
        {
            var mappedUser = this.mapper.Map<SaveUser, User>(newUser);
            var result = await this.userService.CreateUser(mappedUser);

            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Created($"users/{mappedUser.Id}", this.CreateUserResponse(result.Value));
            }

            else
            {
                return BadRequest(result.ValidationErrors);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Authenticate([FromBody] LoginDto userLogin)
        {
            var authResult = await this.userService.ValidateLogin(userLogin.Email, userLogin.Password);

            if (authResult.Status == Ardalis.Result.ResultStatus.Invalid)
            {
                return Unauthorized(authResult.ValidationErrors.First());
            }

            var userDto = this.CreateUserResponse(authResult.Value);

            return Ok(userDto);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserAdrressDto>>> GetAllUsers()
        {
            var users = await this.userService.GetUsers();
            return Ok(this.mapper.Map<List<UserAdrressDto>>(users));
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<UserAdrressDto>> GetUser([FromRoute] int userId)
        {
            return this.mapper.Map<UserAdrressDto>(await this.userService.GetUserById(userId));
        }

        private UserDto CreateUserResponse(User user)
        {
            var userDto = this.mapper.Map<UserDto>(user);
            var token = this.tokenService.GenerateToken(user);

            userDto.Token = token;
            return userDto;
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        [Authorize]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            var deleteResult = await this.userService.DeleteUser(userId);
            if (deleteResult.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return BadRequest("Usuário não existe no banco de dados!");
            }

            return Ok(deleteResult.Value);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("{userId}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserAdrressDto user)
        {
            var updateResult = await this.userService.UpdateUser(userId, this.mapper.Map<User>(user));
            if (updateResult.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return BadRequest("Usuário não existe no banco de dados!");
            }

            return Ok(updateResult.Value);
        }

        [HttpPost]
        [Route("{userId}/places")]
        [Authorize]
        public async Task<ActionResult> CreatePlace([FromRoute] int userId, [FromBody] Address address)
        {
            await this.userService.UpdatePlace(userId, address);
            return Ok();
        }
    }
}
