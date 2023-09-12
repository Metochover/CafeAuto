using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewCafeAuto.DTO.CafeDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;

        public CafeController(NewCafeAutoContext newCafeAutoContext, IMapper mapper)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("createCafe")]
        public async Task<IActionResult> createCafe(AddCafeDTO addCafeDTO)
        {
            Cafe cafe = new Cafe();

            cafe.Name = addCafeDTO.Name;
            cafe.PhoneNumber = addCafeDTO.PhoneNumber;
            cafe.Email = addCafeDTO.Email;
            cafe.CompanyId = addCafeDTO.CompanyId;

            this.newCafeAutoContext.Add(cafe);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<CafeDTO>(cafe));
        }


    }
}
