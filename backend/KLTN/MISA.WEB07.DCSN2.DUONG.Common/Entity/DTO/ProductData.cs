using Microsoft.AspNetCore.Http;

namespace KLTN.Common.Entity.DTO
{
    public class ProductData
    {
        public Product product { get; set; }
        public List<Category>? selectCategory { get; set; }

    }
}
