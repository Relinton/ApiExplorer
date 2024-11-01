using ApiExplorer.JWT__Consumo.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiExplorer.JWT__Consumo.Service
{
    public class ServiceAluno
    {
        private readonly HttpClient _httpClient;
        private const string NomeDeUsuario = "Relinton";
        private const string Password = "123456";

        public ServiceAluno(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7254");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Aluno>> AutenticarEBuscarDadosAsync()
        {
            var credenciais = new { NomeDeUsuario, Password };
            var content = new StringContent(JsonSerializer.Serialize(credenciais), Encoding.UTF8, "application/json");

            var respostaDaAutenticacao = await _httpClient.PostAsync("/api/autenticacao/login", content);
            respostaDaAutenticacao.EnsureSuccessStatusCode();

            var json = await respostaDaAutenticacao.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            var token = tokenResponse["token"];

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var dadosRetornados = await _httpClient.GetAsync("/api/alunos");
            dadosRetornados.EnsureSuccessStatusCode();

            var jsonDados = await dadosRetornados.Content.ReadAsStringAsync();
            var alunos = JsonSerializer.Deserialize<List<Aluno>>(jsonDados, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return alunos;
        }
    }
}
