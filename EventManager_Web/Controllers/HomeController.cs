using System.Diagnostics;
using EventManager_Web.Models;
using EventManager_Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManager_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestaoEventosService _apiService;

        public HomeController(GestaoEventosService apiService)
        {
            _apiService = apiService;

        }

       

        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                ViewBag.Role = "user";
            }

            var eventos = await _apiService.GetEventosAsync();

            return View(eventos);
        }

        public async Task<IActionResult> Login()
        {
            if (Request.Cookies["UserToken"] != null)
            {
                ViewBag.Role = "user";
            }

            return View();
        }

        public async  Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("UserToken"); // Remove o cookie


            var eventos = await _apiService.GetEventosAsync();

            return View("Index", eventos);
        }



        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register( registerModel usuario)
        {
            var sucesso = await _apiService.Register(usuario);

            if (sucesso)
            {
                return RedirectToAction("Login");
            }

            return BadRequest("Falha ao registrar usuário.");
        }

        [HttpPost]
        public async Task<IActionResult> LoginObj(loginModel usuario)
        {
            var sucesso = await _apiService.Login(usuario);

            var userRole = await _apiService.GetUserRoleAsync(usuario.Email);

        

            if (sucesso)
            {
                Response.Cookies.Append("UserToken", userRole, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddHours(1)
                });

                ViewBag.SuccessMessage = "✅ Login realizado com sucesso!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "❌ Credenciais inválidas.");
            }

            return View("Login");
        }


        public async Task<IActionResult> Eventos()
        {

            if (Request.Cookies["UserToken"] != null)
            {
                ViewBag.Role = "user";
            }


            var eventos = await _apiService.GetEventosAsync();

            return View(eventos);

        }
        [HttpPost]
        public async Task<IActionResult> CriarEvento(Evento novoEvento)
        {
        
             await _apiService.CriarEventoAsync(novoEvento);
             return RedirectToAction("Index"); // Redireciona para a lista de evento
    
        }

        [HttpPost]
        public async Task<IActionResult> DeletarEvento(int eventoId)
        {
            await _apiService.DeleteEventoAsync(eventoId);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> AtualizarEvento(int eventoId)
        {
            var evento = await _apiService.GetEventosIDAsync(eventoId);

            ViewBag.Locais = await _apiService.GetLocaisAsync();

            return  View(evento);
        }


        [HttpPost]
        public async Task<IActionResult> AtualizarEventoUpdate(Evento evento)
        {
             await _apiService.AtualizarEvento(evento);
            ViewBag.Locais = await _apiService.GetLocaisAsync();

            return View("AtualizarEvento", evento);
        }




        public async Task<IActionResult> CriarEvento()
        {
            var locais = await _apiService.GetLocaisAsync();
            ViewBag.Locais = locais;

            return View(new Evento()); // Envia um modelo vazio
        }

   

        public IActionResult Checkout()
        {
            return View();
        }

    }
}