using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiControllers.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace ApiControllers.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;
        public ReservationController(IRepository repo) => repository = repo;
        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;
        [HttpGet("{id}")]
        public Reservation Get(int id) => repository[id];
        [HttpPost]
        public Reservation Post([FromBody] Reservation res) => repository.AddReservation(new Reservation
        {
            ClientName = res.ClientName,
            Location = res.Location
        });
        [HttpPut]
        public IActionResult Put([FromBody] Reservation res) => Ok(repository.UpdateReservation(res));
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
