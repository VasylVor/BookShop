﻿using DomainBookShop.Abstract;
using DomainBookShop.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUi.Controllers;
using WebUi.HtmlHelpers;
using WebUi.Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1"},
                new Book{BookId = 2, Name = "Book2"},
                new Book{BookId = 3, Name = "Book3"},
                new Book{BookId = 4, Name = "Book4"},
                new Book{BookId = 5, Name = "Book5"},
            });

            //act
            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel) controller.List(null,2).Model;

            List<Book> books = result.Books.ToList();
            Assert.IsTrue(books.Count == 2);

            Assert.AreEqual(books[0].Name, "Book4");
            Assert.AreEqual(books[1].Name, "Book5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDlegate = i => "Page" + i;

            //act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDlegate);

            //confirmation
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1"},
                new Book{BookId = 2, Name = "Book2"},
                new Book{BookId = 3, Name = "Book3"},
                new Book{BookId = 4, Name = "Book4"},
                new Book{BookId = 5, Name = "Book5"},
            });

            //act
            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);

        }

        [TestMethod]
        public void Can_Filter_Books()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1", Genre = "Genre1"},
                new Book{BookId = 2, Name = "Book2", Genre = "Genre2"},
                new Book{BookId = 3, Name = "Book3", Genre = "Genre1"},
                new Book{BookId = 4, Name = "Book4", Genre = "Genre3"},
                new Book{BookId = 5, Name = "Book5", Genre = "Genre2"},
            });

            //act
            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

           List<Book> result = ((BooksListViewModel)controller.List("Genre2", 1).Model).Books.ToList();


            Assert.AreEqual(result.Count(),2);
            Assert.IsTrue(result[0].Name == "Book2" && result[0].Genre == "Genre2");
            Assert.IsTrue(result[1].Name == "Book5" && result[1].Genre == "Genre2");

        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1", Genre = "Genre1"},
                new Book{BookId = 2, Name = "Book2", Genre = "Genre2"},
                new Book{BookId = 3, Name = "Book3", Genre = "Genre1"},
                new Book{BookId = 4, Name = "Book4", Genre = "Genre3"},
                new Book{BookId = 5, Name = "Book5", Genre = "Genre2"},
            });

            NavController target = new NavController(mock.Object);

            //act
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();


            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");
        }


        [TestMethod]
        public void Can_Selected_Genre()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1", Genre = "Genre1"},
                new Book{BookId = 2, Name = "Book2", Genre = "Genre2"},
                new Book{BookId = 3, Name = "Book3", Genre = "Genre1"},
                new Book{BookId = 4, Name = "Book4", Genre = "Genre3"},
                new Book{BookId = 5, Name = "Book5", Genre = "Genre2"},
            });

            NavController target = new NavController(mock.Object);

            string genreToSelect = "Gnere2";


            //act
            string result = target.Menu(genreToSelect).ViewBag.SelectedGenre;

            Assert.AreEqual(genreToSelect, result);

        }

        [TestMethod]
        public void Generate_Genre_Specific_Book_Count()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{BookId = 1, Name = "Book1", Genre = "Genre1"},
                new Book{BookId = 2, Name = "Book2", Genre = "Genre2"},
                new Book{BookId = 3, Name = "Book3", Genre = "Genre1"},
                new Book{BookId = 4, Name = "Book4", Genre = "Genre3"},
                new Book{BookId = 5, Name = "Book5", Genre = "Genre2"},
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((BooksListViewModel)controller.List("Genre1").Model).PagingInfo.TotalItems;
            int res2 = ((BooksListViewModel)controller.List("Genre2").Model).PagingInfo.TotalItems;
            int res3 = ((BooksListViewModel)controller.List("Genre3").Model).PagingInfo.TotalItems;
            int resAll = ((BooksListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
