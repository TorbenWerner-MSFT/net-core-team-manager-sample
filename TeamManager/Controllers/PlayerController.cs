using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TeamManager.Data;
using TeamManager.Data.Models;
using TeamManager.Models;

namespace TeamManager.Controllers
{
    public class PlayerController : Controller
    {
        private TeamContext _teamContext;

        public PlayerController(TeamContext teamContext)
        {
            _teamContext = teamContext;
        }

        public IActionResult Index()
        {
            var players = _teamContext
                .Players
                .ToList();

            return View(players);
        }

        public IActionResult Detail(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            return View(player);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var teams = _teamContext
                .Teams
                .ToList();

            var model = new CreatePlayerViewModel
            {
                Teams = teams
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind("TeamId", "Name", "ShirtNumber", "Position")] CreatePlayerViewModel createPlayerModel)
        {
            if (ModelState.IsValid)
            {
                var player = createPlayerModel.GetPlayer();

                _teamContext.Players.Add(player);
                _teamContext.SaveChanges();

                return RedirectToAction("Index");
            }

            var teams = _teamContext
                .Teams
                .ToList();

            createPlayerModel.Teams = teams;

            return View(createPlayerModel);
        }
    }
}
