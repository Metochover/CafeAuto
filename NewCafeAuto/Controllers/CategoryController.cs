using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCafeAuto.DTO.CategoryDTO;
using NewCafeAuto.Models;
using System.Runtime.InteropServices;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;
        public CategoryController(NewCafeAutoContext newCafeAutoContext, IMapper mapper) 
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("createCategory")]
        public async Task<IActionResult> createCategory(AddCategoryDTO addCategoryDTO)
        {
            Category category = new Category();

            category.SubId = addCategoryDTO.SubId;
            category.Name = addCategoryDTO.Name;
            category.Description = addCategoryDTO.Description;
            category.Photo = addCategoryDTO.Photo;
            category.MenuId = addCategoryDTO.MenuId;

            this.newCafeAutoContext.Add(category);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<CategoryDTO>(category));
        }

        [HttpGet("showCategory")]
        public async Task<IActionResult> showCategory(int id)
        {

            List<Category> categories = new List<Category>();
            List<CategoryDTO> categoryDTOs = new List<CategoryDTO>();
            
            var category = await newCafeAutoContext.Category                
                .FirstOrDefaultAsync(e => e.Id == id);

            if (category == null)
            {
                return BadRequest("Category not found");
            }
            

            var categoryDTO = mapper.Map<CategoryDTO>(category);

            categoryDTO.SubCategory = categories.Select(subCategory => mapper.Map<CategoryDTO>(subCategory)).ToList();
                

            return Ok(categoryDTO);
        }
    }
}
