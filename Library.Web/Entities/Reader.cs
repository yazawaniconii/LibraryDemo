using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class Reader
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [StringLength(1), Column(TypeName = "char(1)")]
        public string Sex { get; set; }

        public ReaderType Type { get; set; }

        [MaxLength(20)]
        public string Dept { get; set; }

        [StringLength(11), Column(TypeName = "char(11)")]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        // [Column(TypeName = "DateTime(0)")]
        public DateTime DateReg { get; set; }

        [Column(TypeName = "blob")]
        public byte[] Photo { get; set; }

        [StringLength(2), Column(TypeName = "char(2)")]
        public string Status { get; set; }

        [DefaultValue(0)]
        public int BorrowQty { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }

        public int AdminRole { get; set; }
    }
}