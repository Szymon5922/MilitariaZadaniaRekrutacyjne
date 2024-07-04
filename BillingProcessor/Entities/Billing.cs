using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor.Entities
{
    public class Billing
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal value { get; set; }
        [Required]
        public string typeId { get; set; }
        [Required]
        public int tax { get; set; }
        [MaxLength(45)]
        public string? orderId { get; set; }
        public string? offerId { get; set; }
        public string? storeId { get; set; }
    }
}
