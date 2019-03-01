using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using System;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.Infrastructure.Repositories;
using AutoMapper;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using CarPark.Api.Infrastructure.Services;

namespace CarPark.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        public IMapper Mapper;
        public UserRepository Userrep;
        public ILogger<UsersController> ILogger;
        public UserService Userservice;

        public UsersController(IMapper mapper, UserRepository userrep, ILogger<UsersController> ilogger, UserService userservice)
        {

            Mapper = mapper;
            Userrep = userrep;
            ILogger = ilogger;
            Userservice = userservice;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            try
            {
                var user = Userservice.Authenticate(userParam.UserName, userParam.PasswordHash);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                return Ok(Mapper.Map<UserModel>(user));
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = Userrep.GetUsers();

                if (result == null)
                    return NotFound();
                return Ok(Mapper.Map<IEnumerable<UserModel>>(result));


            }

            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }

        [HttpGet]
        [Route("{userid}", Name = "UserGet")]
        public IActionResult Get(string userid)
        {
            try
            {

                var result = Userrep.GetUser(userid);
                if (result == null)
                    return NotFound($"User with id {userid} was not found");
                return Ok(Mapper.Map<UserModel>(result));


            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UserModel user)
        {
            try
            {
                Userrep.Add(Mapper.Map<User>(user));
                await Userrep.SaveAllAsync();
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }

            return BadRequest();

        }

        [HttpPut("{userid}")]
        public async Task<IActionResult> Put(string userid, [FromBody] UserModel user)
        {
            try
            {
                var oldUser = Userrep.GetUser(userid);

                if (oldUser == null)
                    return NotFound($"User with id {userid} not found");
                Mapper.Map<User, User>(Mapper.Map<User>(user), oldUser);
                await Userrep.SaveAllAsync();
                return Ok(Mapper.Map<UserModel>(oldUser));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }

            return BadRequest($"Couldnt update user with id {userid}");


        }

        [HttpPatch("{userid}")]
        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<UserModel> usermodelPatch, string userid)
        {
            try
            {
                var oldUser = Userrep.GetUser(userid);
                if (oldUser == null)
                    return NotFound($"User with id {userid} not found");
                var userPatch = Mapper.Map<JsonPatchDocument<User>>(usermodelPatch);
                userPatch.ApplyTo(oldUser);
                await Userrep.SaveAllAsync();
                return Ok(Mapper.Map<UserModel>(oldUser));
            }
            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update user with id {userid}");


        }

        [HttpDelete("{userid}")]
        public async Task<IActionResult> Delete(string userid)
        {
            try
            {
                var oldUser = Userrep.GetUser(userid);

                if (oldUser == null)
                    return NotFound($"User with id {userid} not found");
                Userrep.Remove(userid);
                await Userrep.SaveAllAsync();
                return Ok(Mapper.Map<UserModel>(oldUser));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }

            return BadRequest($"Couldnt delete user with id {userid}");


        }






    }
}