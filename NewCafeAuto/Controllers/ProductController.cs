using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCafeAuto.DTO.ProductDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;

        public ProductController(NewCafeAutoContext newCafeAutoContext, IMapper mapper)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> addProduct(AddProductDTO addProductDTO)
        {
            Product product = new Product();

            product.Name = addProductDTO.Name;
            product.Description = addProductDTO.Description;
            product.Photo = addProductDTO.Photo;
            product.CategoryId = addProductDTO.CategoryId;

            this.newCafeAutoContext.Add(product);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<ProductDTO>(product));
        }

    }
}
