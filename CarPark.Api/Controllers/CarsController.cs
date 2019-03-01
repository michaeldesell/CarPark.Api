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
    [Route("api/users/{userid}/carparks/{carparkid}/floors/{floorid}/parkingspaces/{parkingspaceid}/car")]
    [ApiController]

    public class CarsController : Controller
    {
        public IMapper Mapper;
        public CarparkRepository Carprep;
        public UserRepository Userrep;
        public FloorRepository Floorrep;
        public ParkingspaceRepository Parkspacerep;
        public CarRepository Carrep;
        public ILogger<CarsController> ILogger;

        public CarsController(IMapper mapper, CarparkRepository carprep, UserRepository userrep, FloorRepository floorrep, ParkingspaceRepository parkspacerep, CarRepository carrep, ILogger<CarsController> ilogger)
        {
            Mapper = mapper;
            Carprep = carprep;
            Userrep = userrep;
            Floorrep = floorrep;
            Parkspacerep = parkspacerep;
            Carrep = carrep;
            ILogger = ilogger;
        }

        [HttpGet]

        public IActionResult Get(int parkingspaceid)
        {
            try
            {

                var result = Carrep.GetCarByParkingSpace(parkingspaceid);
                if (result == null)
                    return NotFound("That parkingspace doesnt have a car");
                return Ok(Mapper.Map<CarModel>(result));
            }

            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }


        [HttpGet]
        [Route("{carid}", Name = "CarGet")]

        public IActionResult Get(int parkingspaceid, int carid)
        {
            try
            {

                var result = Carrep.GetCar(carid);
                if (!result.Parkingspace.Id.Equals(parkingspaceid))
                    return NotFound($"Cant find a car connected to that parkingspace");
                return Ok(Mapper.Map<CarModel>(Mapper.Map<CarModel>(result)));


            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();
        }

        [HttpPost]

        public async Task<IActionResult> Post(int parkingspaceid, int carparkid, [FromBody]CarModel car)
        {

            try
            {

                Parkingspace parkingspace = Parkspacerep.GetParkingspace(parkingspaceid);
                Carpark carpark = Carprep.GetCarpark(carparkid);
                var newcar = Mapper.Map<Car>(car);
                newcar.Parkingspace = parkingspace;
                newcar.Carpark = carpark;
                Carrep.Add(newcar);
                await Carrep.SaveAllAsync();
                return CreatedAtAction(nameof(Get), new { id = newcar.Id }, car);
            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest();

        }

        [HttpPut("{carid}")]

        public async Task<IActionResult> Put(int carid, [FromBody] CarModel car)
        {
            try
            {
                var oldCar = Carrep.GetCar(carid);

                if (oldCar == null)
                    return NotFound($"Car with id {carid} not found");
                Mapper.Map<CarModel, Car>(car, oldCar);
                await Carrep.SaveAllAsync();
                return Ok(Mapper.Map<CarModel>(Mapper.Map<CarModel>(oldCar)));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update car with id {carid}");


        }

        [HttpPatch("{carid}")]

        public async Task<IActionResult> Patch([FromBody] JsonPatchDocument<CarModel> carmodelPatch, int carid)
        {
            try
            {
                var oldCar = Carrep.GetCar(carid);
                if (oldCar == null)
                    return NotFound($"Car with id {carid} not found");
                var carPatch = Mapper.Map<JsonPatchDocument<Car>>(carmodelPatch);
                carPatch.ApplyTo(oldCar);
                await Carrep.SaveAllAsync();
                return Ok(Mapper.Map<CarModel>(oldCar));
            }
            catch (Exception e)
            {

                ILogger.LogError(e.ToString());
            }

            return BadRequest($"Couldnt update car with id {carid}");


        }

        [HttpDelete("{carid}")]

        public async Task<IActionResult> Delete(int carid)
        {
            try
            {
                var oldCar = Carrep.GetCar(carid);

                if (oldCar == null)
                    return NotFound($"Car with id {carid} not found");
                Carrep.Remove(oldCar);
                await Carrep.SaveAllAsync();
                return Ok(Mapper.Map<CarModel>(oldCar));

            }
            catch (Exception e)
            {
                ILogger.LogError(e.ToString());
            }
            return BadRequest($"Couldnt delete car with id {carid}");
        }


    }
}