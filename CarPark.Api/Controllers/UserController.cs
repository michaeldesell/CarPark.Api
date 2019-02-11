using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using CarPark.Api.Models;


namespace CarPark.Api.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public UserController(IOptions<SettingsModel> app, CarParkDbContext context)
        {
          

                                 

        }
    }
}