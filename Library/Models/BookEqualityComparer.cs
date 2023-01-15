using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class BookEqualityComparer : IEqualityComparer<Book>
    {
        public bool Equals(Book x, Book y)
        {
            if (x == null && y == null) return true;
            else if (x == null || y == null) return false;
            else return !x.Name.Equals(y.Name);
        }

        public int GetHashCode(Book obj)
        {
            return obj.GetHashCode();
        }
    }
}