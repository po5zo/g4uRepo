using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using g4u.Controllers.Resources;
using g4u.Core;
using g4u.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace g4u.Controllers
{
    [Route("/api/products/{productId}/photos")]
    public class PhotoController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        public PhotoController(IHostingEnvironment host, IProductRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.host = host;
        }
        [HttpPost]
        public async Task<IActionResult> Uploads(int productId, IFormFile file)
        {
            var product = await repository.Get(productId);
            if (product == null)
                return NotFound();

            if (file == null) return BadRequest("File cannot be null.");
            if (file.Length == 0) return BadRequest("File cannot be empty");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size is exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);
            
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            product.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }
    }
}