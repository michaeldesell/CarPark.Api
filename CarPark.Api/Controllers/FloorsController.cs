using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.Api.Infrastructure.Repositories;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.ApplicationCore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace CarPark.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/users/{userid}/carparks/{carparkid}/[controller]")]
    [ApiController]
    public class FloorsController : Controller
    {
        public IMapper Mapper;
        public CarparkRepository Carprep;
        public UserRepository Userrep;
        public FloorRepository Floorrep;
        public ILogger<FloorsController> ILogger;

        public FloorsController(IMapper mapper, CarparkRepository carprep, UserRepository userrep, FloorRepository floorrep, ILogger<FloorsController> ilogger)
        {
            Mapper = mapper;
            Carprep = carprep;
            Userrep = userrep;
            Floorrep = floorrep;
            ILogger = ilogger;

        }

        [HttpGet]

        public IActionResult Get(int carparkid)
        {
            try
            {

                var result = Floorrep.GetFloorsByCarpark(carparkid);
                if (result == null)
                    return NotFound("That carpark doesnt have any floors");
                return Ok(Mapper.Map<FloorModel>(result));
            }

            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }
        [HttpGet]
        [Route("{floorid}", Name = "FloorGet")]

        public IActionResult Get(int carparkid, int floorid)
        {
            try
            {

                var result = Floorrep.GetFloor(floorid);
                if (!result.Carpark.Id.Equals(carparkid))
                    return NotFound($"Cant find a floor connected to that carpark");
                return Ok(Mapper.Map<FloorModel>(result));


            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }

        [HttpPost]

        public async Task<IActionResult> Post(int carparkid, [FromBody]FloorModel floor)
        {

            try
            {

                Carpark carpark = Carprep.GetCarpark(carparkid);
                var newfloor = Mapper.Map<Floor>(floor);
                newfloor.Carpark = carpark;
                Floorrep.Add(newfloor);
                await Floorrep.SaveAllAsync();
                return CreatedAtAction(nameof(Get), new { id = newfloor.Id }, floor);
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }

        [HttpPut("{floorid}")]

        public async Task<IActionResult> Put(int floorid, [FromBody] FloorModel floor)
        {
            try
            {
                var oldFloor = Floorrep.GetFloor(floorid);

                if (oldFloor == null)
                    return NotFound($"Floor with id {floorid} not found");
                Mapper.Map<FloorModel, Floor>(floor, oldFloor);
                await Floorrep.SaveAllAsync();
                return Ok(Mapper.Map<FloorModel>(oldFloor));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update floor with id {floorid}");


        }

        [HttpPatch("{floorid}")]

        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<FloorModel> floormodelPatch, int floorid)
        {
            try
            {
                var oldFloor = Floorrep.GetFloor(floorid);
                if (oldFloor == null)
                    return NotFound($"Floor with id {floorid} not found");
                var floorPatch = Mapper.Map<JsonPatchDocument<Floor>>(floormodelPatch);
                floorPatch.ApplyTo(oldFloor);
                await Floorrep.SaveAllAsync();
                return Ok(Mapper.Map<FloorModel>(oldFloor));
            }
            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update floor with id {floorid}");


        }

        [HttpDelete("{floorid}")]

        public async Task<IActionResult> Delete(int floorid)
        {
            try
            {
                var oldFloor = Floorrep.GetFloor(floorid);

                if (oldFloor == null)
                    return NotFound($"Floor with id {floorid} not found");
                Floorrep.Remove(oldFloor);
                await Floorrep.SaveAllAsync();
                return Ok(Mapper.Map<FloorModel>(oldFloor));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }
            return BadRequest($"Couldnt delete floor with id {floorid}");
        }
    }
}