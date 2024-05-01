using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("orders")]
    public class Order
    {
        [Key]
        public Guid? order_id { get; set; }
        public DateTime? order_date { get; set; }  //  'Ngày đơn hàng được tạo',
        public DateTime? from_date { get; set; }  
        public DateTime? to_date { get; set; }  
        public decimal? total_order_deposit { get; set; }  // 'Tổng tiền cọc của đơn hàng',
        public decimal? total_order_payment { get; set; }  // 'tổng số tiền thanh toán của đơn hàng',
        public decimal? total_order_return { get; set; }  // 'tổng số tiền được trả lại = tổng số tiền cọc - tổng số tiền được thanh toán',
        public int? status { get; set; }  // 'Trạng thái của đơn hàng',
        public Guid? user_id { get; set; }  // 'id của khách hàng',
        public string? user_name { get; set; }  // 'tên khách hàng',
        public string? phone_number { get; set; }
        public string? address { get; set; }
        public string? description { get; set; }  // 'ghi chú của đơn hàng',
        public int? payment_type { get; set; }
        public int? order_type { get; set; }
    }
}
