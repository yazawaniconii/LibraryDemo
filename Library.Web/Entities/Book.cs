using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Author { get; set; }

        [MaxLength(50)]
        public string Press { get; set; }

        public DateTime DatePress { get; set; }

        [MaxLength(15)]
        public string Isbn { get; set; }

        [MaxLength(30)]
        public string Catalog { get; set; }

        public int Language { get; set; }

        public int Pages { get; set; }

        [Column(TypeName = "decimal(4,1)")]
        public decimal Price { get; set; }

        public DateTime DateIn { get; set; }

        public string Brief { get; set; }

        [Column(TypeName = "blob")]
        public byte[] Cover { get; set; }

        [StringLength(2), Column(TypeName = "char(2)")]
        public string Status { get; set; }

    }
}