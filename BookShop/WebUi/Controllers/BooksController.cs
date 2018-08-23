using DomainBookShop.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUi.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }
       
        public ViewResult List()
        {
            return View(_repository.Books);
        }
    }
}