using Lesson2Task1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lesson2Task1.Controllers
{
    public class LoginController : Controller
    {
        // Db simulation
        private List<User> _users = new List<User>
        {
            new User { Id = 0, Username = "admin", Password = "admin123", IsAdmin = true },
            new User { Id = 1, Username = "user", Password = "user123", IsAdmin = false }
        };
        public IActionResult Verify()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Verify(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both username and password.";
                return View();
            }

            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }
            var userJson = JsonSerializer.Serialize(user);
            HttpContext.Session.SetString("User", userJson);

            return RedirectToAction("Index", "Home");
        }
    }
}
