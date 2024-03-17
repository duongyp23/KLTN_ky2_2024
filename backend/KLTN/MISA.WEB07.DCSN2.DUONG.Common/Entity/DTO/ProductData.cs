using Microsoft.AspNetCore.Http;

namespace KLTN.Common.Entity.DTO
{
    public class ProductData:Product
    {
        public List<IFormFile>? Images { get; set; }

    }
}
