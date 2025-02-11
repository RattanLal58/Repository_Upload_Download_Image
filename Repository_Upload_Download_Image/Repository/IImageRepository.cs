using Repository_Upload_Download_Image.Models;

namespace Repository_Upload_Download_Image.Repository
{
    public interface IImageRepository
    {
        Task<IEnumerable<ImageModel>> GetAllImagesAsync();
        Task<ImageModel> GetImageByIdAsync(int id);
        Task AddImageAsync(ImageModel image);
        Task UpdateImageAsync(ImageModel image);
        Task DeleteImageAsync(int id);
    }
}
