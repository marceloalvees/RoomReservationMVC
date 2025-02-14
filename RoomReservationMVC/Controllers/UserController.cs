using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RoomReservationMVC.Models;

namespace RoomReservationMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:44354/api/user";

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }

            var json = JsonSerializer.Serialize(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonSerializer.Deserialize<MessageResponse<object>>(jsonResponse);

            if (messageResponse.success)
            {
                return RedirectToAction("Reservation");
            }
            else
            {
                TempData["ErrorMessage"] = messageResponse.message;
                ModelState.AddModelError(string.Empty, "Erro ao enviar os dados.");
                return View("Index", user);
            }
        }
    }
}
