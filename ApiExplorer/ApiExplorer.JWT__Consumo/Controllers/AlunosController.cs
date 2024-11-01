using ApiExplorer.JWT__Consumo.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiExplorer.JWT__Consumo.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ServiceAluno _serviceAluno;

        public AlunosController(ServiceAluno serviceAluno)
        {
            _serviceAluno = serviceAluno;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var dadosRetornados = await _serviceAluno.AutenticarEBuscarDadosAsync();
                return View(dadosRetornados);
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar os dados da api.");
            }
        }
    }
}
