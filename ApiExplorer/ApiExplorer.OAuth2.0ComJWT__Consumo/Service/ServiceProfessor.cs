using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ApiExplorer.OAuth2._0ComJWT__Consumo.Models;

namespace ApiExplorer.OAuth2._0ComJWT__Consumo.Service
{
    public class ServiceProfessor
    {
        private readonly HttpClient _httpClient;
        private const string Username = "admin";
        private const string Password = "123";

        public ServiceProfessor(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7112");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Professor>> AutenticarEBuscarDadosAsync()
        {
            var credenciais = new { Username, Password };
            var content = new StringContent(JsonSerializer.Serialize(credenciais), Encoding.UTF8, "application/json");

            var respostaDaAutenticacao = await _httpClient.PostAsync("/api/auth/login", content);
            respostaDaAutenticacao.EnsureSuccessStatusCode();

            var json = await respostaDaAutenticacao.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            var token = tokenResponse["token"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var dadosRetornados = await _httpClient.GetAsync("/api/professores");
            dadosRetornados.EnsureSuccessStatusCode();

            var jsonDados = await dadosRetornados.Content.ReadAsStringAsync();
            var professores = JsonSerializer.Deserialize<List<Professor>>(jsonDados, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return professores;
        }
    }
}
