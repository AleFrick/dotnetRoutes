using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRoutes.Controllers
{
    [Route("api/usuario/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class Teste : ControllerBase
    {        
        const string route = "/api/usuario/";
        [HttpGet($"{route}get")]
        public string Get()
        {
            return "get";
        }
        [HttpGet($"{route}getall")]
        public string GetAll()
        {
            return "GetAll";
        }
        [HttpPost($"{route}setuser")]
        public string SetUsuario()
        {
            return "setusuario";
        }
    }
}
