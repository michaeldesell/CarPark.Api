using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarPark.Api.ApplicationCore.Entities;
using CarPark.Api.ApplicationCore.Models;
using CarPark.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarPark.Api.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/users/{userid}/carparks/{carparkid}/floors/{floorid}/[controller]")]
    [ApiController]

    public class ParkingspacesController : Controller
    {
        public IMapper Mapper;
        public CarparkRepository Carprep;
        public UserRepository Userrep;
        public FloorRepository Floorrep;
        public ParkingspaceRepository Parkspacerep;
        public ILogger<ParkingspacesController> ILogger;

        public ParkingspacesController(IMapper mapper, CarparkRepository carprep, UserRepository userrep, FloorRepository floorrep, ParkingspaceRepository parkspacerep,ILogger<ParkingspacesController> ilogger)
        {
            Mapper = mapper;
            Carprep = carprep;
            Userrep = userrep;
            Floorrep = floorrep;
            Parkspacerep = parkspacerep;
            ILogger = ilogger;
        }

        [HttpGet]

        public IActionResult Get(int floorid)
        {
            try
            {

                var result = Parkspacerep.GetPSpaceByFloor(floorid);
                if (result == null)
                    return NotFound("That floor doesnt have any parkingspaces");
                return Ok(Mapper.Map<IEnumerable<ParkingspaceModel>>(result));
            }

            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }
        [HttpGet]
        [Route("{parkingspaceid}", Name = "ParkingspacesGet")]

        public IActionResult Get(int floorid, int parkingspaceid)
        {
            try
            {

                var result = Parkspacerep.GetPSpaceByFloor(floorid);
                if (result == null)
                    return NotFound($"Floor with id {floorid} doesnt have any parkingspaces");

                var parkingspace = result.OfType<Parkingspace>().Where(ps => ps.Id == parkingspaceid).FirstOrDefault();
                if (parkingspace == null)
                    NotFound($"Floor with id {floorid} doesnt have a parkingspace with that id");
                
                return Ok(Mapper.Map<ParkingspaceModel>(parkingspace));


            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }

        [HttpPost]

        public async Task<IActionResult> Post(int floorid, [FromBody]ParkingspaceModel parkingspace)
        {

            try
            {

                Floor floor = Floorrep.GetFloor(floorid);
                var newparkingspace = Mapper.Map<Parkingspace>(parkingspace);
                newparkingspace.Floor = floor;
                Parkspacerep.Add(newparkingspace);
                await Parkspacerep.SaveAllAsync();
                return CreatedAtAction(nameof(Get), new { id = newparkingspace.Id }, parkingspace);
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }

            return BadRequest();

        }

        [HttpPut("{parkingspaceid}")]

        public async Task<IActionResult> Put(int parkingspaceid, [FromBody] ParkingspaceModel parkingspace)
        {
            try
            {
                var oldParkingspace = Parkspacerep.GetParkingspace(parkingspaceid);

                if (oldParkingspace == null)
                    return NotFound($"Parkingspace with id {parkingspace} not found");
                Mapper.Map<ParkingspaceModel, Parkingspace>(parkingspace, oldParkingspace);
                await Parkspacerep.SaveAllAsync();
                return Ok(Mapper.Map<ParkingspaceModel>(oldParkingspace));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }

            return BadRequest($"Couldnt update parkingspace with id {parkingspaceid}");


        }

        [HttpPatch("{parkingspaceid}")]

        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<ParkingspaceModel> parkingspacemodelPatch, int parkingspaceid)
        {
            try
            {
                var oldParkingspace = Parkspacerep.GetParkingspace(parkingspaceid);
                if (oldParkingspace == null)
                    return NotFound($"Parkingspace with id {parkingspaceid} not found");
                var parkingspacePatch = Mapper.Map<JsonPatchDocument<Parkingspace>>(parkingspacemodelPatch);
                parkingspacePatch.ApplyTo(oldParkingspace);
                await Parkspacerep.SaveAllAsync();
                return Ok(Mapper.Map<ParkingspaceModel>(oldParkingspace));
            }
            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update parkingspace with id {parkingspaceid}");


        }

        [HttpDelete("{parkingspaceid}")]

        public async Task<IActionResult> Delete(int parkingspaceid)
        {
            try
            {
                var oldParkingspace = Parkspacerep.GetParkingspace(parkingspaceid);

                if (oldParkingspace == null)
                    return NotFound($"Parkingspace with id {parkingspaceid} not found");
                Parkspacerep.Remove(oldParkingspace);
                await Parkspacerep.SaveAllAsync();
                return Ok(Mapper.Map<ParkingspaceModel>(oldParkingspace));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());

            }
            return BadRequest($"Couldnt delete parkingspace with id {parkingspaceid}");
        }

    }
}