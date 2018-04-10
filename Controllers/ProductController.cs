using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using g4u.Controllers.Resources;
using g4u.Core;
using g4u.Core.Models;
using g4u.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace g4u.Controllers
{
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Category> categoryRepository;
        private readonly IRepository<Platform> platformRepository;   
        public ProductController(IMapper mapper, IUnitOfWork unitOfWork, IRepository<Platform> platformRepository, IRepository<Category> categoryRepository, IProductRepository productRepository)
        {
            this.platformRepository = platformRepository;
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProduct([FromBody] SaveProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = mapper.Map<SaveProductResource, Product>(productResource);
            product.CreateDate = DateTime.Now;
            product.LastUpdate = DateTime.Now;
            product.DeleteDate = null;
            productRepository.Add(product);
            await unitOfWork.CompleteAsync();
            product = await productRepository.Get(product.Id);
            var result = mapper.Map<Product, ProductResource>(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productRepository.Get(id);
            if (product == null)
                return NotFound();
            productRepository.Remove(product);
            await unitOfWork.CompleteAsync();
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await productRepository.Get(id);
            if (product == null)
                return NotFound();
            var ProductResource = mapper.Map<Product, ProductResource>(product);
            return Ok(ProductResource);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] SaveProductResource productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await productRepository.Get(id);

            if (product == null)
                return NotFound();

            mapper.Map<SaveProductResource, Product>(productResource, product);
            product.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            product = await productRepository.Get(product.Id);
            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpGet]
        public async Task<QueryResultResource<ProductResource>> GetProducts(ProductQueryResource filterResource)
        {
            var filter = mapper.Map<ProductQueryResource, ProductQuery>(filterResource);
            var queryResult = await productRepository.GetProductsAsync(filter);

            return mapper.Map<QueryResult<Product>, QueryResultResource<ProductResource>>(queryResult);
        }
        [HttpGet("categories")]
        public async Task<IEnumerable<CategoryResource>> GetCategories()
        {
            var categories = await categoryRepository.GetAll();
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
        }
        [HttpGet("platforms")]
        public async Task<IEnumerable<PlatformResource>> GetPlatforms()
        {
            var platforms = await platformRepository.GetAll();
            return mapper.Map<IEnumerable<Platform>, IEnumerable<PlatformResource>>(platforms);
        }
        [HttpGet("platforms/{id}")]
        public async Task<IActionResult> GetPlatform(int id)
        {
            var platform = await platformRepository.Get(id);
            if (platform == null)
                return NotFound();
            var PlatformResource = mapper.Map<Platform, PlatformResource>(platform);
            return Ok(PlatformResource);
        }
        [HttpGet("categories/{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await categoryRepository.Get(id);
            if (category == null)
                return NotFound();
            var CategoryResource = mapper.Map<Category, CategoryResource>(category);
            return Ok(CategoryResource);
        }


    }
}