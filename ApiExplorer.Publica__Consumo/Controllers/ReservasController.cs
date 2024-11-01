using ApiExplorer.Publica__Consumo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiExplorer.Publica__Consumo.Controllers
{
    public class ReservasController : Controller
    {
        private readonly string apiUrl = "https://localhost:7268/api/reservas";

        public async Task<IActionResult> Index()
        {
            List<Reserva> listaReservas = [];
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    listaReservas = JsonConvert.DeserializeObject<List<Reserva>>(apiResponse);
                }
            }
            return View(listaReservas);
        }

        public ViewResult Buscar() => View();

        [HttpPost]
        public async Task<IActionResult> Buscar(int id)
        {
            Reserva reserva = new();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reserva = JsonConvert.DeserializeObject<Reserva>(apiResponse);
                }
            }
            return View(reserva);
        }

        public ViewResult AdicionarReserva() => View();

        [HttpPost]
        public async Task<IActionResult> AdicionarReserva(Reserva reserva)
        {
            Reserva reservaRecebida = new Reserva();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(reserva), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservaRecebida = JsonConvert.DeserializeObject<Reserva>(apiResponse);
                }
            }
            return View(reservaRecebida);
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(int id)
        {
            Reserva reserva = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl + "/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reserva = JsonConvert.DeserializeObject<Reserva>(apiResponse);
                }
            }
            return View(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(Reserva reserva)
        {
            Reserva reservaRecebida = new();

            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent
                {
                    { new StringContent(reserva.ReservaId.ToString()), "ReservaId" },
                    { new StringContent(reserva.Nome), "Nome" },
                    { new StringContent(reserva.InicioLocacao), "InicioLocacao" },
                    { new StringContent(reserva.FimLocacao), "FimLocacao" }
                };

                using (var response = await httpClient.PutAsync(apiUrl, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Sucesso";
                    reservaRecebida = JsonConvert.DeserializeObject<Reserva>(apiResponse);
                }
            }
            return View(reservaRecebida);
        }

        [HttpPost]
        public async Task<IActionResult> Deletar(int ReservaId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(apiUrl + "/" + ReservaId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
