using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("user")]
    public class User
    {
        [Key]
        public Guid? user_id { get; set; } //'id của người dùng',
        public string user_name { get; set; } //'Tên khách hàng',
        
        public string? user_address { get; set; } // 'Địa chỉ của khách hàng',
        public string phone_number { get; set; } // 'số điện thoại của khách hàng',
        public string password { get; set; }//'Mật khẩu mã hóa',
        public DateTime? birthday { get; set; }// 'ngày tháng năm sinh',
        public string? bank_account_number { get; set; }//'Số tài khoản',
        public string? bank_name { get; set; }// 'Tên ngân hàng',
        public string? bank_code { get; set; } //'Mã viết tắt của ngân hàng',
        public string? qr_code_url{ get; set; } //'Ảnh đại diện của khách hàng',
        public int? is_manager { get; set; }//'là quản lý hay không phục vụ phân quyền',
    }
}
