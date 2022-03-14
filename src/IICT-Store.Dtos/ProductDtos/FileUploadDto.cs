using Microsoft.AspNetCore.Http;

namespace IICT_Store.Dtos.ProductDtos
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
    }
}