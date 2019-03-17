using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using WishList.Data;
using WishList.Models;

namespace WishList.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
       
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            var model = _context.Items.Where(i => i.User.Id == currentUser.Id).ToList();

            return View("Index", model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Models.Item item)
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            item.User = currentUser;
            _context.Items.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            var item = _context.Items.FirstOrDefault(e => e.Id == id && e.User.Id == currentUser.Id);
            if (item == null)
            {
                return Unauthorized();
            }
            
            _context.Items.Remove(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
