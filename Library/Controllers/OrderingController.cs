using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.Data.Entity;

namespace Library.Controllers
{
    public class OrderingController : Controller
    {
        LibraryContext lb = new LibraryContext();
        
        public ActionResult Index()
        {
            var forms = lb.OrderForms.Include(p => p.Student).Include(p => p.Book);
            return View(forms.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList students = new SelectList((from s in lb.Students
                                                  select
                                                  new { StudentId = s.Id, Name = s.NumberStudentCard + " " + s.Name + " " + s.Surname }),
                                                  "StudentId",
                                                  "Name");
            ViewBag.Students = students;
            var b0 = lb.Books;

            var b2 = from b in lb.Books
                     join of in lb.OrderForms on b.id equals of.BookId into ob
                     from o in ob.DefaultIfEmpty()
                     where o.ReturnDate == null && o.ReceiptDate == null
                     orderby b.id
                     select b;

            var b3 = from b in lb.Books
                     join of in lb.OrderForms on b.id equals of.BookId into ob
                     from o in ob.DefaultIfEmpty()
                     where o.ReturnDate == null
                     orderby b.id
                     select b;

            var b4 = b3.Except(b2);

            var b5 = b0.Except(b4).OrderBy(b=>b.id);
            
            SelectList books = new SelectList(b5, "id", "Name");
            ViewBag.Books = books;

            return View();

        }
        [HttpPost]
        public ActionResult Create(OrderForm order)
        {
            order.ReceiptDate = DateTime.Now;
            

            order.BookId = order.id;
            lb.OrderForms.Add(order);
            lb.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Statistica()
        {
            List<Statistica> books;
            using(var lc = new LibraryContext())
            {
                books = lc.Database.SqlQuery<Statistica>("Select Distinct " +
                                                         "Book.Name as BookName" + ", " +
                                                         "Count(Order_Form.Book_id) Over(Partition by Book.Name) as Amount " + 
                                                         "From Book Inner Join Order_Form on Book.id = Order_Form.Book_id "
                                                        ).ToList();
            }
            ViewBag.Books = books;
            return View();
        }
    }
}