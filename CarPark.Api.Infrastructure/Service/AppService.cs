using System;
using System.Linq;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CarPark.Api.Infrastructure.EF_Core.DbContext;

namespace CarPark.Api.Infrastructure.Service
{

    public interface iapplicationservice
    {
        applicationlogin Authenticate(string username, string password);
    }

    public class appsettings
    {
        public string secret { get; set; }
    }

    public class applicationlogin
    {
        public int id { get; set; }
        public string AppName { get; set; }
        public string password { get; set; }
        public string Token { get; set; }
    }

    public class applicationservice : iapplicationservice
    {
        private CarParkDbContext _context;
        private readonly appsettings _appsettings;

        public applicationservice(IOptions<appsettings> appsettings, CarParkDbContext context)
        {
            _appsettings = appsettings.Value;
            _context = context;
        }

        public applicationlogin Authenticate(string username, string password)
        {


            //var apps = applogins.FirstOrDefault(x => x.username == username && x.password == password);
            var apps = _context.Users.FirstOrDefault(x => x.Email == username && x.PasswordHash == password);

            if (apps == null)
                return null;
            var Tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.secret);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,apps.Id.ToString())
                     //new Claim(ClaimTypes.Role,apps.id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };

            var token = Tokenhandler.CreateToken(tokendescriptor);
            applicationlogin all = new applicationlogin()
            {
                Token = Tokenhandler.WriteToken(token),
                password = null

            };
            //apps.Token = Tokenhandler.WriteToken(token);
            //apps.password = null;

            return all;
        }
        //public IEnumerable<applicationlogin> GetAll()
        //{
        //    // return users without passwords
        //    return _context.ApplicationLogins.Select(x => {
        //        x.password = null;
        //        return x;
        //    });
        //}


    }
}
