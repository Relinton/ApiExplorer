using ApiExplorer.OAuth2._0ComJWT__Consumo.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiExplorer.OAuth2._0ComJWT__Consumo.Controllers
{
    public class ProfessoresController : Controller
    {
        private readonly ServiceProfessor _serviceProfessor;

        public ProfessoresController(ServiceProfessor serviceProfessor)
        {
            _serviceProfessor = serviceProfessor;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var dadosRetornados = await _serviceProfessor.AutenticarEBuscarDadosAsync();
                return View(dadosRetornados);
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar os dados da api.");
            }
        }
    }
}
