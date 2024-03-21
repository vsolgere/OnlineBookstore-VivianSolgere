using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookstore_VivianSolgere.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookstore_VivianSolgere.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookstoreContext _context;
        private const int PageSize = 10;

        public HomeController(BookstoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNum = 0)
        {
            var totalBooks = await _context.Books.CountAsync();
            var totalPages = (int)System.Math.Ceiling(totalBooks / (double)PageSize);

            var books = await _context.Books
                                      .OrderBy(b => b.Title)
                                      .Skip(pageNum * PageSize)
                                      .Take(PageSize)
                                      .ToListAsync();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pageNum;

            return View(books);
        }
    }
}
