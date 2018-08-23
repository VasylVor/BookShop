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

        public ViewResult List(int page = 1)
        {
            BooksListViewModel model = new BooksListViewModel
            {
                Books = _repository.Books
                .OrderBy(book => book.BookId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _repository.Books.Count()
                }
            };
            return View(model);
        }
    }
}
