using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        [Column("Publication_Date")]
        public DateTime? PublicationDate { get; set; }
        public string Publication { get; set; }
        public string Language { get; set; }

        public List<BookToAuthor> Authors { get; set; }
    }
}