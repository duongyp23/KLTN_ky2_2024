using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace KLTN.Common.Entity
{
    [Table("product")]
    public class Product
    {
        [Key]
        public Guid? product_id { get; set; }
        public string? product_name { get; set; }
        public string? product_code { get; set; }
        public string? product_image_url { get; set; }
        public decimal? product_price { get; set; }
        public decimal? rental_price { get; set; }
        public decimal? sell_price { get; set; }
        public int? status { get; set; }
        public string? status_name { get; set; }
        public string? description { get; set; }
        public string? product_list_img_url { get; set; } 
    }
}
