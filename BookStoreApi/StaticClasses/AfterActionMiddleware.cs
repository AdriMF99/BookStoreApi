using Microsoft.AspNetCore.SignalR;
using ProjectTracker.SignalR.Services.Hubs;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BookStoreApi.StaticClasses
{
    public class AfterActionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHubContext<RealTimeUpdateHub> _hubContext;
        private readonly HttpClient _httpClient;

        public AfterActionMiddleware(RequestDelegate next, IHubContext<RealTimeUpdateHub> hubContext, HttpClient httpClient)
        {
            _next = next;
            _hubContext = hubContext;
            _httpClient = httpClient;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            // nombre del usuario logeado
            string userName;
            if (context.User.Identity.IsAuthenticated)
            {
                userName = context.User.FindFirstValue("name");
            }
            else
            {
                userName = "Usuario no logueado";
            }

            // ruta de la solicitud
            var requestPath = context.Request.Path;

            // método HTTP de la solicitud
            var requestMethod = context.Request.Method;
            var tiempo = DateTime.UtcNow.ToString();

            string formattedUserName = $"\u001b[1;32m{userName}\u001b[0m";
            string formattedRequest = $"\u001b[1;31m{requestMethod} {requestPath}\u001b[0m";
            string formattedTiempo = $"\u001b[1;34m{tiempo}\u001b[0m";

            string message = $"Usuario: {formattedUserName}, Acción del controlador: {formattedRequest}, Hora: {formattedTiempo}";
            string messageNoColor = $"Usuario: {userName}, Acción del controlador: {requestMethod} {requestPath}, Hora: {tiempo}";

            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);

            //Console.WriteLine(message);
            //Console.WriteLine("------------------------------------------------------------------------------------------------------");

            var projectData = new
            {
                ProjectName = "My Awesome Project",
                Description = "A brief description of the project."
            };
            string projectDataJson = JsonSerializer.Serialize(projectData);

            string apiUrl = $"https://localhost:7054/api/Code/recibir-mensaje?message={Uri.EscapeDataString(messageNoColor)}";
            string apiUrl2 = "https://localhost:7054/api/Project/postProject";

            var content = new StringContent(projectDataJson);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response2 = await _httpClient.PostAsync(apiUrl2, content);
            }
            catch (Exception ex)
            {

            }
            
            // Envía la solicitud HTTP al endpoint
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            //HttpResponseMessage response2 = await _httpClient.GetAsync(apiUrl2);
            if (response2.IsSuccessStatusCode)
            {
                Console.WriteLine("Endpoint usado correctamente! :)");

            }
            else
            {
                Console.WriteLine("No se pudo usar el endpoint! :(");
            }
        }
    }
}
