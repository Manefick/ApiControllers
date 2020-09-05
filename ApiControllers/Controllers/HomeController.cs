using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;

namespace ApiControllers.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Reservations);
        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            repository.AddReservation(reservation);
            return RedirectToAction("Index");
        }
    }
}
