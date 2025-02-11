using Microsoft.EntityFrameworkCore;
using Repository_Upload_Download_Image.Models;

namespace Repository_Upload_Download_Image.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<ImageModel>ImageModels { get; set; }
    }
}
