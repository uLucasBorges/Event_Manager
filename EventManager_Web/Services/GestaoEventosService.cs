using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using EventManager_Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventManager_Web.Services
{
    public class GestaoEventosService
    {
        public readonly HttpClient _httpClient;

        public GestaoEventosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7123/");
        }

  

        public async Task<IEnumerable<Evento>> GetEventosAsync()
        {
            var response = await _httpClient.GetAsync("api/eventos");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Evento>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Evento> GetEventosIDAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/eventos/{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Evento>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<Local> GetLocaisIDAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/locais/{id}");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Local>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }


        public async Task<IEnumerable<Local>> GetLocaisAsync()
        {
            var response = await _httpClient.GetAsync("api/locais");
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Local>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }



    

        public async Task<bool> CriarLocalAsync(Local novoLocal)
        {
            var jsonLocal = JsonSerializer.Serialize(novoLocal);
            var content = new StringContent(jsonLocal, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/locais", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }

        public async Task<bool> CriarEventoAsync(Evento novoEvento)
        {
            var jsonEvento = JsonSerializer.Serialize(novoEvento);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/eventos", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }

        public async Task<bool> DeleteEventoAsync(int eventoId)
        {

            var content = new StringContent("", Encoding.UTF8, "application/json"); // Corpo vazio
            var response = await _httpClient.PostAsync($"api/eventos/deletar/{eventoId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarEvento(Evento novoEvento)
        {
            var jsonEvento = JsonSerializer.Serialize(novoEvento);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/eventos/atualizar", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }


        public async Task<bool> DeletarLocalAsync(int eventoId)
        {

            var content = new StringContent("", Encoding.UTF8, "application/json"); // Corpo vazio
            var response = await _httpClient.PostAsync($"api/locais/deletar/{eventoId}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarLocal(Local novoEvento)
        {
            var jsonEvento = JsonSerializer.Serialize(novoEvento);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/locais/atualizar", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }


        public async Task<bool> Register(registerModel usuario)
        {
            var jsonEvento = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("register", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }

        public async Task<bool> Login(loginModel usuario)
        {
            var jsonEvento = JsonSerializer.Serialize(usuario);
            var content = new StringContent(jsonEvento, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("login", content);

            return response.IsSuccessStatusCode; // Retorna true se o evento foi criado com sucesso
        }


        public async Task<string> GetUserRoleAsync(string email)
        {
            var json = JsonSerializer.Serialize(new { email = email });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Permissoes", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }


    }
}
