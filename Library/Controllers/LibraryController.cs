using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Library.Models;

namespace Library.Controllers
{
    public class LibraryController : Controller
    {
        LibraryContext lb = new LibraryContext();
        
        public ActionResult Index(string book, int? author)
        {

            IEnumerable<Book> booksData = lb.Books.Include(b => b.Authors.Select(bta => bta.Author));
            if (!String.IsNullOrEmpty(book) && !book.Equals("Всі"))
            {
                booksData = booksData.Where(b => b.Name.Equals(book));
            }
            if (author != null)
            {

                booksData = from b in booksData
                            where b.Authors.Select(bta => bta.Author).Where(a => a.id == author).FirstOrDefault() != null
                            select b;
            }

            SelectList authors = new SelectList((from a in lb.Authors
                                                 select
                                                 new { id = a.id, Name = a.Name + " " + a.Surname }),
                                                 "id",
                                                 "Name");

            var boo = (from b in lb.Books
                       select new { Name = b.Name }).Distinct();

            SelectList books = new SelectList(boo, "Name", "Name");
            ViewBag.Authors = authors;
            ViewBag.Books = books;

            return View(booksData.ToList());
        }


        [HttpGet]
        public ActionResult Create()
        {


            SelectList authors = new SelectList((from a in lb.Authors
                                                 orderby a.Surname
                                                 select new { id = a.id, Name = a.Name + " " + a.Surname }),
                                                 "id",
                                                 "Name");
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book, List<int> authors)
        {
            if (authors != null && authors.Count > 0 && book != null)
            {
                book.Authors = new List<BookToAuthor>();
                foreach (int a in authors)
                {
                    Author author = lb.Authors.Find(a);
                    if (author != null)
                    {
                        book.Authors.Add(
                            new BookToAuthor
                            {
                                Author = author,
                                Book = book
                            }
                            );

                    }
                }
            }
            if (book != null)
            {
                lb.Books.Add(book);
                lb.SaveChanges();
            }
            return RedirectToAction("Index", "Library");
        }

        [HttpGet]
        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAuthor(Author author)
        {
            if (author != null)
            {
                lb.Authors.Add(author);
                lb.SaveChanges();
            }
            return RedirectToAction("Index", "Library");
        }

        public ActionResult Delete(int id)
        {
            Book book = lb.Books.Find(id);
            if (book != null)
            {
                lb.Books.Remove(book);
                lb.SaveChanges();
            }
            return RedirectToAction("Index", "Library");
        }
    }
}