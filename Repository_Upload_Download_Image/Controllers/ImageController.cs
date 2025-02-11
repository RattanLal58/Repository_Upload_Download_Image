using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repository_Upload_Download_Image.Models;
using Repository_Upload_Download_Image.Repository;

namespace Repository_Upload_Download_Image.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageRepository _imageRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageController(IImageRepository imageRepository, IWebHostEnvironment hostEnvironment)
        {
            _imageRepository = imageRepository;
            _hostEnvironment = hostEnvironment;
        }

       
        public IActionResult Upload()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
           
            if (file != null && file.Length > 0)
            {
                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);

              
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

              
                var image = new ImageModel
                {
                    FileName = fileName,
                    FilePath = Path.Combine("images", fileName)
                };

                await _imageRepository.AddImageAsync(image);

                return RedirectToAction("Index");
            }

            return View();
        }

       
        public async Task<IActionResult> Download(int id)
        {
            var image = await _imageRepository.GetImageByIdAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            var filePath = Path.Combine(_hostEnvironment.WebRootPath, image.FilePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", image.FileName);
        }

       
        public async Task<IActionResult> Index()
        {
            var images = await _imageRepository.GetAllImagesAsync();
            return View(images);
        }

       
        public async Task<IActionResult> Delete(int id)
        {
            await _imageRepository.DeleteImageAsync(id);
            return RedirectToAction("Index");
        }
    }

}
