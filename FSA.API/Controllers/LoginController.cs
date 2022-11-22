using FSA.API.Business;
using FSA.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace FSA.API.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {

        private IConfiguration _configuration;
        public LoginController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                LoginLogic login = new LoginLogic();
                var validatedLogin = login.ValidateLogin(model);
                if (validatedLogin == null) return Unauthorized();


                string forKey = _configuration.GetValue<string>("Authentication:SecretForKey");

                var bytes = Encoding.ASCII.GetBytes(
                    forKey
                    );
                SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("Identity", validatedLogin.ID.ToString()));
                claims.Add(new Claim("sub", validatedLogin.ID.ToString()));
                claims.Add(new Claim("Role", validatedLogin.Role));

                var jwt = new JwtSecurityToken(
                     _configuration.GetValue<string>("Authentication:Issuer"),
                     _configuration.GetValue<string>("Authentication:Audience"),

                      claims,
                      DateTime.UtcNow,
                      DateTime.UtcNow.AddMinutes(20),
                      signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
        //[HttpPost]
        //public ActionResult Login([FromForm] LoginModel login)
        //{
        //    //HttpRequest request = new HttpRequest(HttpMethod.Post);
        //    // LoginModel login;

        //    if (ModelState.IsValid)
        //    {
        //        //login = new LoginModel { Username = userName, Password = passWord };
        //        user = GetUser(login);
        //    }

        //    else
        //        return BadRequest();

        //    if (user == null)
        //        return Unauthorized();
        //    string forKey = _configuration.GetValue<string>("Authentication:SecretForKey");
        //    //create token
        //    var bytes = Encoding.ASCII.GetBytes(
        //        forKey
        //        // _configuration["Authentication:SecretForKey"]
        //        );
        //    SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);
        //    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //    var claims = new List<Claim>();
        //    claims.Add(new Claim("sub", user.UserID.ToString()));
        //    claims.Add(new Claim("Role", user.Role));

        //    var jwt = new JwtSecurityToken(
        //         _configuration.GetValue<string>("Authentication:Issuer"),
        //         _configuration.GetValue<string>("Authentication:Audience"),
        //          //_configuration.Properties["Authentication:Issuer"].ToString(),
        //          //_configuration.Properties["Authentication:Audience"].ToString(),
        //          claims,
        //          DateTime.UtcNow,
        //          DateTime.UtcNow.AddMinutes(20),
        //          signingCredentials);

        //    var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    return Ok(token);
        //}
        //        private User GetUser(LoginModel login)
        //        {
        //            User user = new User("johnwtwco", "John Elmerson", "Esguerra", "Admin");
        //            if (ValidateUser(login))
        //                return user;
        //#pragma warning disable CS8603 // Possible null reference return.
        //            return null;
        //#pragma warning restore CS8603 // Possible null reference return.
        //        }

        private bool ValidateUser(LoginModel login)
        {
            return true;
        }
    }
}
