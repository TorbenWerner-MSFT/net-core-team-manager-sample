using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TeamManager.Data;
using TeamManager.Models;

namespace TeamManager.Controllers
{
    public class PlayerController : Controller
    {
        private readonly TeamContext _teamContext;

        public PlayerController(TeamContext teamContext)
        {
            _teamContext = teamContext;
        }

        public IActionResult Index()
        {
            var players = _teamContext
                .Players
                .OrderBy(o => o.ShirtNumber)
                .ToList();

            return View(players);
        }

        public IActionResult Detail(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            if (player == null)
                return NotFound();

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
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TeamId", "Name", "ShirtNumber", "Position")] CreatePlayerViewModel createPlayerModel)
        {
            // Not Valid, return form
            if (!ModelState.IsValid)
            {
                var teams = _teamContext
                    .Teams
                    .ToList();

                createPlayerModel.Teams = teams;

                return View(createPlayerModel);
            }

            // Valid, save data
            var player = createPlayerModel.GetPlayer();

            _teamContext.Players.Add(player);
            _teamContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            if (player == null)
                return NotFound();

            var teams = _teamContext
                .Teams
                .ToList();

            var model = new EditPlayerViewModel(player)
            {
                Teams = teams
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id", "TeamId", "Name", "ShirtNumber", "Position")] EditPlayerViewModel editPlayerModel)
        {
            // Not Valid, return form
            if (!ModelState.IsValid)
            {
                var teams = _teamContext
                    .Teams
                    .ToList();

                editPlayerModel.Teams = teams;

                return View(editPlayerModel);
            }

            // Valid, save data
            var player = editPlayerModel.GetPlayer();

            _teamContext.Players.Update(player);
            _teamContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            if (player == null)
                return NotFound();

            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePlayer(int id)
        {
            var player = _teamContext
                .Players
                .Include(player => player.Team)
                .FirstOrDefault(player => player.Id == id);

            if (player == null)
                return NotFound();

            _teamContext.Players.Remove(player);
            _teamContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
