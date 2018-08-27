using DomainBookShop.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUi.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository Repository { get; set; }

        public NavController(IBookRepository repository)
        {
            Repository = repository;
        }
        // GET: Nav
        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;

            IEnumerable<string> genres = Repository.Books
                .Select(book => book.Genre)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
}