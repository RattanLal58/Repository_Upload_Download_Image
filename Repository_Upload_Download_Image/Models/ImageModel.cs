using System.ComponentModel.DataAnnotations;

namespace Repository_Upload_Download_Image.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
    }
}
