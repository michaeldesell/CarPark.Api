using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Services;
using CarPark.Api.Infrastructure.Repositories;
using AutoMapper;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace CarPark.Api.Controllers
{
    //[Authorize]
    [Produces("application/json")]
    [Route("api/users/{userid}/[controller]")]
    [ApiController]
    public class CarparksController : ControllerBase
    {
        public IMapper Mapper;
        public CarparkRepository Carprep;
        public UserRepository Userrep;
        public CarRepository Carrep;
        public ILogger<UsersController> ILogger;

        public CarparksController(IMapper mapper, CarparkRepository carprep,UserRepository userrep,CarRepository carrep,ILogger<UsersController> ilogger)
        {

            Mapper = mapper;
            Carprep = carprep;
            Userrep = userrep;
            Carrep = carrep;
            ILogger = ilogger;
        }

        [HttpGet]

        public IActionResult Get(string userid)
        {
            try
            {
                var result = Carprep.GetCarparksByUser(userid);
                if (result == null)
                    return NotFound("That user doesnt have any carparks");
                return Ok(Mapper.Map<IEnumerable<CarparkModel>>(result));
            }

            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }
        [HttpGet]
        [Route("{carparkid}", Name = "CarparkGet")]

        public IActionResult Get(string userid, int carparkid)
        {
            try
            {

                var result = Carprep.GetCarpark(carparkid);
                if (!result.User.Id.Equals(userid))
                    return NotFound($"Cant find a carpark connected to that user");
                return Ok(Mapper.Map<CarparkModel>(result));
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("{carparkid}/cars")]
        public IActionResult GetCars(int carparkid)
        {
            try
            {

                var result = Carrep.GetCarsByCarpark(carparkid);
                if (result == null)
                    return NotFound("That carpark doesnt have any cars");
                return Ok(Mapper.Map<IEnumerable<CarModel>>(result));
            }

            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }
        [HttpPost]

        public async Task<IActionResult> Post(string userid, [FromBody]CarparkModel carpark)
        {

            try
            {

                User user = Userrep.GetUser(userid);
                var newcarpark = Mapper.Map<Carpark>(carpark);
                newcarpark.User = user;
                Carprep.Add(newcarpark);
                await Carprep.SaveAllAsync();
                return CreatedAtAction(nameof(Get), new { id = newcarpark.Id }, carpark);
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }
        [HttpPut("{carparkid}")]

        public async Task<IActionResult> Put(string userid, int carparkid, [FromBody] CarparkModel carpark)
        {
            try
            {
                var oldCarpark = Carprep.GetCarpark(carparkid);

                if (carparkid == 0)
                    return NotFound($"Carpark with id {carparkid} not found");
                Mapper.Map<CarparkModel, Carpark>(carpark, oldCarpark);

                await Carprep.SaveAllAsync();
                return Ok(Mapper.Map<CarparkModel>(oldCarpark));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update carpark with id {carparkid}");


        }

        [HttpPatch("{carparkid}")]

        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<CarparkModel> carparkmodelPatch, int carparkid)
        {
            try
            {
                var oldCarpark = Carprep.GetCarpark(carparkid);
                if (oldCarpark == null)
                    return NotFound($"Carpark with id {carparkid} not found");
                var carparkPatch = Mapper.Map<JsonPatchDocument<Carpark>>(carparkmodelPatch);
                carparkPatch.ApplyTo(oldCarpark);
                await Carprep.SaveAllAsync();
                return Ok(Mapper.Map<CarparkModel>(oldCarpark));
            }
            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update carpark with id {carparkid}");


        }

        [HttpDelete("{carparkid}")]

        public async Task<IActionResult> Delete(int carparkid)
        {
            try
            {
                var oldCarpark = Carprep.GetCarpark(carparkid);

                if (oldCarpark == null)
                    return NotFound($"Carpark with id {carparkid} not found");
                Carprep.Remove(oldCarpark);
                await Carprep.SaveAllAsync();
                return Ok(Mapper.Map<CarparkModel>(oldCarpark));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }
            return BadRequest($"Couldnt carpark user with id {carparkid}");
        }
    }
}