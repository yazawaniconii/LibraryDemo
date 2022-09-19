using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models
{
    public class LoginUser
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}