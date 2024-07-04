using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingProcessor.Entities
{
    public class Order
    {
        [Column("id")]
        public int id { get; set; }
        [Required]
        [MaxLength(45)]
        public string orderId { get; set; }
        public int? erpOrderId { get; set; }
        public int? invoiceId { get; set; }
        public int? storeId { get; set; }
    }
}
