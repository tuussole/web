using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Order_Form")]
    public class OrderForm
    {
        [Key]
        public int id { get; set; }
        [Column("Receipt_Date")]
        public DateTime? ReceiptDate { get; set; }
        [Column("Return_Date")]
        public DateTime? ReturnDate { get; set; }


        [Column("Student_id")]
        public int? StudentId { get; set; }
        public Student Student { get; set; }


        [Column("Book_id")]
        public int? BookId { get; set; }
        public Book Book { get; set; }
    }
}