using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCafeAuto.Models;
using NewCafeAuto.DTO.MenuDTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using NewCafeAuto.DTO.CategoryDTO;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;

        public MenuController(NewCafeAutoContext newCafeAutoContext, IMapper mapper)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("createMenu")]
        public async Task<IActionResult> createMenu(AddMenuDTO addMenuDTO)
        {
            DateTime dateTime = DateTime.Now;
            Menu menu = new Menu();

            menu.Name = addMenuDTO.Name;
            menu.Description = addMenuDTO.Description;
            menu.IsAvailable = true;
            menu.CreatedDate = dateTime.ToString("dd.MM.yyyy");
            menu.UpdatedDate = "";
            menu.CafeId = addMenuDTO.CafeId;

            var cafe = await newCafeAutoContext.Cafe.FirstOrDefaultAsync(e => e.Id == addMenuDTO.CafeId);
            if (cafe == null) 
            {
                return BadRequest("Cafe not found");
            }
            
            this.newCafeAutoContext.Add(menu);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<MenuDTO>(menu));
        }

        [HttpGet("showMenu")]
        public async Task<IActionResult> showMenu(int id)
        {
            var menu = await newCafeAutoContext.Menu
                .Include(e => e.Category).ThenInclude(e => e.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(menu == null)
            {
                return BadRequest("Menu not found");
            }

            return Ok(mapper.Map<MenuDTO>(menu));
        }

    }
}
