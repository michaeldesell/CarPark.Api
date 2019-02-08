
using CarPark.Api.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPark.Api.Controllers
{

    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {


        private iapplicationservice _applicationservice;

        public AuthsController(iapplicationservice applicationservice)
        {
           
            _applicationservice = applicationservice;

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public ActionResult<applicationlogin> Authenticate([FromBody]applicationlogin appparams)
        {

            var appslogins = _applicationservice.Authenticate(appparams.AppName, appparams.password);
            if (appslogins == null)
                return NoContent();

            return Ok(
           appslogins);
            
           }

        [Authorize(Roles = "CarParkWeb")]
        [HttpGet]
        [Route("TestAuthorization")]
        public ActionResult TestAuthorization()
        {

            

            return Ok();

        }
        // GET api/values
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
