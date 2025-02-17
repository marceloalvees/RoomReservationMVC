using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RoomReservationMVC.Models;

namespace RoomReservationMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:44354/api/Reservation";

        public ReservationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(ReservationModel reservation)
        {
            var json = JsonSerializer.Serialize(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonSerializer.Deserialize<MessageResponse<object>>(jsonResponse);

            if (messageResponse.success)
            {
                return RedirectToAction("User", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = messageResponse.message;
                ModelState.AddModelError(string.Empty, "Erro ao enviar os dados.");
                return View("Index", reservation);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Put(UpdateReservationModel reservation)
        {
            reservation.Status = EReservationStatus.Canceled;
            var json = JsonSerializer.Serialize(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_apiUrl, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var messageResponse = JsonSerializer.Deserialize<MessageResponse<object>>(jsonResponse);
            if (messageResponse.success)
            {
                return RedirectToAction("User", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = messageResponse.message;
                ModelState.AddModelError(string.Empty, "Erro ao enviar os dados.");
                return View("Index", reservation);
            }
        }
    }
}
