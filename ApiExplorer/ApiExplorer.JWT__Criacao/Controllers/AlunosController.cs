using ApiExplorer.JWT__Criacao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiExplorer.JWT__Criacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public ActionResult<List<Aluno>> Listar()
        {
            var alunos = new List<Aluno>
            {
                new() { Id = Guid.NewGuid(), Nome = "Relinton" },
                new() { Id = Guid.NewGuid(), Nome = "João" },
                new() { Id = Guid.NewGuid(), Nome = "Paulo" },
                new() { Id = Guid.NewGuid(), Nome = "Pedro" },
                new() { Id = Guid.NewGuid(), Nome = "Thiago" },
                new() { Id = Guid.NewGuid(), Nome = "Jeremias" }
            };

            return Ok(alunos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Aluno> ObterPorId(int id)
        {
            var aluno = new Aluno { Id = new Guid(), Nome = "Relinton" };

            return Ok(aluno);
        }
    }
}
