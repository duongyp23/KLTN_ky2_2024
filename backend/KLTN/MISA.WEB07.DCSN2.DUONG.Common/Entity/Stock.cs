using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("stock")]
    public class Stock
    {
        [Key]
        public Guid? stock_id { get; set; }
        public Guid? product_id { get; set; }
        public string? option_code { get; set; }
        public int? total_quantity { get; set; }
        public int? quantity_rental { get; set; }
        public int? quantity_sold { get; set; }
        public int? quantity_remain { get; set; }

    }
}
