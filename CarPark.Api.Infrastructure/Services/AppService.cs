using System;
using System.Linq;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CarPark.Api.Infrastructure.EF_Core.DBContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace CarPark.Api.Infrastructure.Services
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
          
            var apps = _context.Users.FirstOrDefault(x => x.UserName == username && x.PasswordHash == password);

            if (apps == null)
                return null;
            var Tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.secret);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetLoginClaims(apps)),
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

        public List<Claim> GetLoginClaims(ApplicationCore.Entities.User app)
        {
           

            IdentityOptions _options = new IdentityOptions();
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, app.UserName),
            new Claim(_options.ClaimsIdentity.UserIdClaimType, app.Id.ToString()),
            new Claim(_options.ClaimsIdentity.UserNameClaimType, app.UserName)
        };
            var userClaims = _context.UserClaims.Where(x => x.UserId == app.Id).Select(x=>new Claim(x.ClaimType,x.ClaimValue)).ToList();
            var roleslink = _context.UserRoles.Where(x => x.UserId == app.Id).Select(x =>x.RoleId).ToList();
            var userroles = _context.Roles.Where(x => roleslink.Contains(x.Id));
            var roleClaims = _context.RoleClaims.Where(x => roleslink.Contains(x.RoleId)).ToList();
            claims.AddRange(userClaims);
            claims.AddRange(userroles.Select(x => new Claim(ClaimTypes.Role,x.Name)));
            claims.AddRange(roleClaims.Select(x => new Claim(x.ClaimType, x.ClaimValue)));
            
            return claims;
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
