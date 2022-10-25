using Microsoft.AspNetCore.Mvc;
using dotnetRoutes.Models;
using dotnetRoutes.Services;

namespace dotnetRoutes.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class Login : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Login(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IResult getLogin(ModelLogin login)
        {
            ModelToken mdlToken = new ModelToken();
            mdlToken.Usuario = login.Usuario;
            mdlToken.Role = "admin";                        

            string token = TokenService.GenerateToken(_configuration["Jwt:Password"]);
            return Results.Ok(new
            {
                token = token
            });
        }
    }
}
