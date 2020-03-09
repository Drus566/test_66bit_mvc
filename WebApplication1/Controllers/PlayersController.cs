using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<ChatHub> _hub;
        [BindProperty]
        public Player Player { get; set; }
        [BindProperty]
        public Player Team { get; set; }
        public PlayersController(ApplicationDbContext db, IHubContext<ChatHub> hub)
        {
            _db = db;
            _hub = hub;
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
        public async Task<IActionResult> Upsert()
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

                string id = Player.Id.ToString();
                string name = Player.Name;
                string surname = Player.Surname;
                string birth = Player.Birth;
                string team = _db.Teams.FirstOrDefault(t => t.TeamId == Player.TeamId).Name;
                string gender = Player.Gender.ToString();
                string country = Player.Country.ToString();

                // Пытался отрендерить партиал GetPlayer и отдать в SendAsync как строку,
                // но не нашел как рендерить html в строку

                await _hub.Clients.All.SendAsync("ReceiveMessage", id, name, surname, birth, team, gender, country);
                return RedirectToAction("Index");
            }
            return View(Player);
        }
    }
}