using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KLTN.Common.Entity
{
    [Table("category")]
    public class Category
    {
        /// <summary>
        /// ID nhãn dán
        /// </summary>
        [Key]
        public Guid? category_id { get; set; }

        /// <summary>
        /// Mã nhãn dán
        /// </summary>
        public string? category_code { get; set; }
        public string? description { get; set; }
        public string? type_name { get; set; }
        public int?   type { get; set; }
    }
}
