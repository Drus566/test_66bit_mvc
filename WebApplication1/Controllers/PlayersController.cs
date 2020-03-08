using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Player Player { get; set; }
        [BindProperty]
        public Player Team { get; set; }
        public PlayersController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        // GET: Players
        public IActionResult Index()
        {
            var players = _db.Players.ToList();
            if (players != null)
            {
                foreach (Player p in players)
                {
                    _db.Teams.Where(t => t.TeamId == p.TeamId).Load();
                }
                return View(players);
            }
            return NotFound();
        }

        public IActionResult Upsert(int? id)
        {
            var teams = _db.Teams.ToList();
            ViewBag.Teams = new SelectList(teams, "TeamId", "Name");

            // create
            if (id == null)
            {
                Player = new Player();
                return View(Player);
            }
            // update
            Player = _db.Players.FirstOrDefault(u => u.Id == id);
            if (Player == null)
            {
                return NotFound();
            }
            return View(Player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
             
                if (Player.Id == 0)
                {
                    //create
                    _db.Players.Add(Player);
                }
                else
                {
                    _db.Players.Update(Player);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Player);
        }
    }
}