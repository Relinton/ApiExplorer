using ApiExplorer.OAuth2._0ComJWT__Criacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiExplorer.OAuth2._0ComJWT__Criacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfessoresController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Professor>> Index()
        {
            var professores = new List<Professor>
            {
                new() { Nome = "Pinheiro" },
                new() { Nome = "Franco" }
            };
            return Ok(professores);
        }
    }
}
