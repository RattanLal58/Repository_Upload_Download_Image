using Microsoft.EntityFrameworkCore;
using Repository_Upload_Download_Image.Data;
using Repository_Upload_Download_Image.Models;

namespace Repository_Upload_Download_Image.Repository
{
    public class ImageRepository:IImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        
        public async Task<IEnumerable<ImageModel>> GetAllImagesAsync()
        {
            return await _context.ImageModels.ToListAsync();
        }

       
        public async Task<ImageModel> GetImageByIdAsync(int id)
        {
            return await _context.ImageModels.FindAsync(id);
        }

       
        public async Task AddImageAsync(ImageModel image)
        {
            _context.ImageModels.Add(image);
            await _context.SaveChangesAsync();
        }

       
        public async Task UpdateImageAsync(ImageModel image)
        {
            _context.ImageModels.Update(image);
            await _context.SaveChangesAsync();
        }

       
        public async Task DeleteImageAsync(int id)
        {
            var image = await GetImageByIdAsync(id);
            if (image != null)
            {
              
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, image.FilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

              
                _context.ImageModels.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
