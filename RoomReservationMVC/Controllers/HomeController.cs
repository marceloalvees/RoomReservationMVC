using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RoomReservationMVC.Models;

namespace RoomReservationMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly string _apiUrl = "https://localhost:44354/api/";
    private readonly HttpClient _httpClient;


    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Reservation()
    {
        var rooms = await GetRooms();
        ViewBag.Email = "marcelloalves09@gmail.com"; //TempData["email"];
        ViewBag.Rooms = rooms;
        return View(rooms);
    }

    public async Task<IActionResult> User()
    {
        var email = "marcelloalves09@gmail.com"; //TempData["email"];
        ViewBag.Email = email;
        ViewBag.Reservations = await GetReservation(email.ToString());
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<List<SelectListItem>> GetRooms()
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}Room", CancellationToken.None);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var messageResponse = JsonSerializer.Deserialize<MessageResponse<List<RoomModel>>>(jsonResponse);

        if (messageResponse != null && messageResponse.success)
        {
            // Converte os dados de RoomModel para SelectListItem
            var rooms = messageResponse.data.Select(room => new SelectListItem
            {
                Text = room.name, 
                Value = room.id.ToString() 
            }).ToList();

            return rooms;
        }
        else
        {

            return new List<SelectListItem>();
        }
    }

    [HttpGet]
    public async Task<List<ReservationResponseModel>> GetReservation(string email)
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}Reservation/{email}");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var messageResponse = JsonSerializer.Deserialize<MessageResponse<List<ReservationResponseModel>>>(jsonResponse);

        if (messageResponse.success)
        {
            
            return messageResponse.data;
        }
        else
        {
            return new List<ReservationResponseModel>();
        }
    }
}
