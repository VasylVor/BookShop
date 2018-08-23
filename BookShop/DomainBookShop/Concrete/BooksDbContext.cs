using DomainBookShop.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBookShop.Concrete
{
    class BooksDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
    }
}
