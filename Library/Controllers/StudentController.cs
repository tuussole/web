using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using System.Data.Entity;

namespace Library.Controllers
{
    public class StudentController : Controller
    {
        LibraryContext lb = new LibraryContext();
        
        public ActionResult Index()
        {
            
            return View(lb.Students.ToList());
        }

        public ActionResult Detail(int id)
        {
            Student student = lb.Students.Find(id);

            var b = lb.OrderForms.Where(o => o.StudentId == id).Include(o => o.Book);

            ViewBag.Books = b;

            return View(student);
        }

        public ActionResult Return(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Student");
            }
            OrderForm of = lb.OrderForms.Find(id);

            of.ReturnDate = DateTime.Now;
            lb.SaveChanges();

            return RedirectToAction("Detail", "Student", new { id = of.StudentId });
        }

        public ActionResult Delete(int? id)
        {
            Student student = lb.Students.Find(id);
            if (id != null && student != null) 
            {
                lb.Students.Remove(student);
                lb.SaveChanges();
            }


            return RedirectToAction("Index", "Student");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            lb.Students.Add(student);
            lb.SaveChanges();

            return RedirectToAction("Index", "Student");
        }
    }
}