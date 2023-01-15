using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Book_To_Author")]
    public class BookToAuthor
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Column("Book_Id")]
        public int? BookId { get; set; }
        public Book Book { get; set; }


        [Column("Author_Id")]
        public int? AuthorId { get; set; }
        public Author Author { get; set; }
    }
}