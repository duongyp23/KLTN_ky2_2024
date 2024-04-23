namespace KLTN.Common.Entity.DTO
{
    /// <summary>
    /// Dữ liệu đơn hàng
    /// </summary>
    public class OrderData
    {
        public Order order { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
        public List<Product>? products { get; set; }

        public static implicit operator OrderData(Order v)
        {
            throw new NotImplementedException();
        }
    }
}
