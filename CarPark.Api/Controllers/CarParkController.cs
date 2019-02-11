using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarPark.Api.Models;

namespace CarPark.Api.Controllers
{
    public class CarParkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("GetUserActiveCarPark")]
        public IActionResult GetUserActiveCarPark(UserModel user)
        {

            


                                 
        }
    }
}