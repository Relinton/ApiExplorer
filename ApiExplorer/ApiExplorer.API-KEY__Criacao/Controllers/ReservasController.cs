using ApiExplorer.API_KEY__Criacao.Models;
using ApiExplorer.API_KEY__Criacao.Models.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ApiExplorer.API_KEY__Criacao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private IReservaRepository _reservaRepository;

        public ReservasController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        [HttpGet]
        public IEnumerable<Reserva> Listar() => _reservaRepository.Reservas;

        [HttpGet("{id}")]
        public Reserva BuscarPorId(int id) => _reservaRepository[id];

        [HttpPost]
        public IActionResult Adicionar([FromBody] Reserva reserva)
        {
            if (!Authenticate())
                return Unauthorized("401 - Não Autorizado");

            return Ok(_reservaRepository.AddReserva(new Reserva
            {
                Nome = reserva.Nome,
                InicioLocacao = reserva.InicioLocacao,
                FimLocacao = reserva.FimLocacao
            }));
        }

        [HttpPut]
        public IActionResult Editar([FromForm] Reserva reserva)
        {
            if (!Authenticate())
            {
                return Unauthorized("401 - Não Autorizado");
            }
            return Ok(_reservaRepository.UpdateReserva(reserva));
        }

        [HttpPatch("{id}")]
        public StatusCodeResult EditarPorId(int id, [FromForm] JsonPatchDocument<Reserva> patch)
        {
            Reserva reserva = BuscarPorId(id);
            if (reserva != null)
            {
                patch.ApplyTo(reserva);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Deletar(int id)
        {
            if (!Authenticate())
            {
                throw new UnauthorizedAccessException("401 - Não Autorizado");
            }
            
            _reservaRepository.DeleteReserva(id);
        }

        bool Authenticate()
        {
            var chavesSecretas = new[] { "reserva@2024", "acesse@e@reserve@" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in chavesSecretas where t == key select t).Count();
            return count == 0 ? false : true;
        }
    }
}
