using ApiExplorer.Publica__Criacao.Models;
using ApiExplorer.Publica__Criacao.Models.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ApiExplorer.Publica__Criacao.Controllers
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
        public Reserva Adicionar([FromBody] Reserva reserva)
        {
            return _reservaRepository.AddReserva(new Reserva
            {
                Nome = reserva.Nome,
                InicioLocacao = reserva.InicioLocacao,
                FimLocacao = reserva.FimLocacao
            });
        }

        [HttpPut]
        public Reserva Editar([FromForm] Reserva reserva) => _reservaRepository.UpdateReserva(reserva);

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
        public void Deletar(int id) => _reservaRepository.DeleteReserva(id);
    }
}
