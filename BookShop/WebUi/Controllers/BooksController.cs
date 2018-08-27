using DomainBookShop.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUi.Models;

namespace WebUi.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _repository;
        public int pageSize = 4;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(string genre, int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = _repository.Books
                .Where(b => genre == null || b.Genre == genre)
                .OrderBy(book => book.BookId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = genre == null ?
                            _repository.Books.Count() :
                            _repository.Books.Where(book => book.Genre == genre).Count()
                },
                CurrentGenre = genre
            };
            return View(model);
        }
    }
}
