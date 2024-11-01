using ApiExplorer.JWT__Criacao.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ApiExplorer.JWT__Criacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AutenticacaoController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            bool resultado = ValidarUsuario(usuario);
            if (resultado)
            {
                var tokenString = GerarTokenJwt();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
        private bool ValidarUsuario(Usuario usuario)
        {
            if (usuario.NomeDeUsuario == "Relinton" && usuario.Password == "123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string GerarTokenJwt()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey
                              (Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,
                                             audience: audience,
                                             expires: DateTime.Now.AddMinutes(120),
                                             signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
