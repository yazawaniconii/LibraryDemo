using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class ReaderType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(20)]
        public string TypeName { get; set; }
        public int CanLendQty { get; set; }
        public int CanLendDay { get; set; }
        public int CanContinueTimes { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal PunishRate { get; set; }
        [Required]
        public int DataValid { get; set; }
    }
}