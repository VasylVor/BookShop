using DomainBookShop.Abstract;
using DomainBookShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBookShop.Concrete
{
    public class BookDbRepository : IBookRepository
    {
        BooksDbContext context = new BooksDbContext();

        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }
    }
}
