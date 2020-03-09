using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Team Team { get; set; }
        public TeamsController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Teams
        public ActionResult Index()
        {

            var teams = _db.Teams.ToList();
            if (teams != null)
            {
                foreach (Team t in teams)
                {
                    _db.Players.Where(p => p.TeamId == t.TeamId).Load();
                }
                return View(teams);
            }
            return NotFound();
        }

        // POST: Teams/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromBody] Team team)
        {
            string name = team.Name.Trim();
            if (!String.IsNullOrWhiteSpace(name) || !String.IsNullOrEmpty(name))
            {
                if (_db.Teams.FirstOrDefault(p => p.Name == name) == null)
                {
                    _db.Teams.Add(team);
                    _db.SaveChanges();
                    return new JsonResult(team);
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult("Team is exists");
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return new JsonResult(string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
        }
    }
}