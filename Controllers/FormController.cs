using dotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace dotNetProject.Controllers
{
    public class FormController : Controller
    {
        private readonly ILogger<FormController> _logger;

        public FormController(ILogger<FormController> logger)
        {
            _logger = logger;
        }

        public IActionResult FormView()
        {
            return View();
        }
    }
}
