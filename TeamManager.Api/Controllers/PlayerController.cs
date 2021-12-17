using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TeamManager.Data;
using TeamManager.Data.Models;

namespace TeamManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private TeamContext _teamContext;

        public PlayerController(TeamContext teamContext)
        {
            _teamContext = teamContext;
        }

        [HttpGet]
        public IActionResult GetPlayers()
        {
            var players = _teamContext
                .Players
                .ToList();

            return Ok(players);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            return Ok(player);
        }

        [HttpPost]
        public IActionResult CreatePlayer(Player player)
        {
            if (ModelState.IsValid)
            {
                _teamContext.Players.Add(player);
                _teamContext.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
