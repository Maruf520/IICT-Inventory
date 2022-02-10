using IICT_Store.Models.Gallery;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Dtos.Gallery
{
    public class GetAvailable
    {
        [Required]
        public DateTime Date { get; set; }
        public GalleryNo GalleryNo { get; set; }
    }
}
