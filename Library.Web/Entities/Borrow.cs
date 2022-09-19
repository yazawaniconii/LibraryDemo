using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class Borrow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Reader Reader { get; set; }

        public Book Book { get; set; }

        public int ContinueTimes { get; set; }

        public DateTime DateOut { get; set; }
        public DateTime DateRetPlan { get; set; }
        public DateTime? DateRetAct { get; set; }

        public int OverDay { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal OverMoney { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal PunishMoney { get; set; }

        [Column(TypeName = "bit")]
        public bool IsReturned { get; set; }

        [MaxLength(20)]
        public string OperatorBorrow { get; set; }

        [MaxLength(20)]
        public string OperatorReturn { get; set; }
    }
}