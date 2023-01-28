using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class BorrowViewModel
    {
        [Required]
        public int ReaderId { get; set; }

        [Required]
        public string BookCode { get; set; }
    }
}