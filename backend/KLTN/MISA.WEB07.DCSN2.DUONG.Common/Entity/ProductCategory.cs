using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("product_category")]
    public class ProductCategory
    {
        [Key]
        public Guid? product_category_id { get; set; }
        public Guid? product_id { get; set; }
        public string product_name { get; set; }
        public string product_code { get; set; }
        /// <summary>
        /// ID nhãn dán
        /// </summary>
        public Guid category_id { get; set; }

        /// <summary>
        /// Mã nhãn dán
        /// </summary>
        public string category_code { get; set; }
    }
}
