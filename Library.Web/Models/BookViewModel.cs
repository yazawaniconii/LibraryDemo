using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class BookViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}