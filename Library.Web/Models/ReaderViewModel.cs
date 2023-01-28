using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class ReaderViewModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public int Sex { get; set; }
        // public ReaderType Type { get; set; }

        [MaxLength(20)]
        public string Dept { get; set; }

        [StringLength(11)]
        public string Phone { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [Required]
        public int Type { get; set; }

        // [Column(TypeName = "DateTime(0)")]
        // public DateTime DateReg { get; set; }

        public byte[] Photo { get; set; }

        public string Status { get; set; }

        // [DefaultValue(0)]
        [Required]
        public int BorrowQty { get; set; }

        [MaxLength(20)]
        [Required]
        public string Password { get; set; }
    }
}