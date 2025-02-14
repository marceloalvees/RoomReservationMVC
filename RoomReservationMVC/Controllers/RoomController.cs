using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RoomReservationMVC.Models;

namespace RoomReservationMVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:44354/api/user";

        public RoomController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonSerializer.Deserialize<MessageResponse<List<RoomModel>>>(jsonResponse);
            if (messageResponse.success)
            {
                return View("Rooms", messageResponse.data);
            }
            else
            {
                TempData["ErrorMessage"] = messageResponse.message;
                return View("Index");
            }
        }
    }
}
