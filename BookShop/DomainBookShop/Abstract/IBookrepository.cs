using DomainBookShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainBookShop.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
    }
}
