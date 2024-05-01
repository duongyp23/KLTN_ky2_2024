using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("order_detail")]
    public class OrderDetail
    {
        [Key]
        public Guid? order_detail_id { get; set; } //'id chi tiết của order',
        public Guid? order_id { get; set; } //'id của đơn hàng',
        public Guid? product_id { get; set; } // 'id của sản phẩm',
        public string? product_code { get; set; } // 'mã sản phẩm',
        public string? product_name { get; set; } // 'tên sản phẩm',
        public decimal? product_deposit { get; set; } // 'số tiền cọc của sản phẩm',
        public decimal? product_rental { get; set; } 
        public decimal? product_payment { get; set; } // 'số tiền thuê của sản phẩm',
        public decimal? product_return { get; set; } // 'Số tiền trả lại của sản phẩm = số tiền cọc - sổ tiền thuê',
        public string? product_image_url { get; set; }
        public int? quantity { get; set; }
        public string? option_code { get; set; }
    }
}
