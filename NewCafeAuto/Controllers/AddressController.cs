using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCafeAuto.DTO.AddressDTO;
using NewCafeAuto.Models;
using System.Security.Claims;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;
        public AddressController(NewCafeAutoContext newCafeAutoContext, IMapper mapper, IConfiguration configuration)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }
        
        [HttpPost("AddAddress")]
        public async Task<IActionResult> AddAddress(AddAddressDTO addAddressDTO)
        {
            Addresses addresses = new Addresses();

            var control = await newCafeAutoContext.Users.FirstOrDefaultAsync(e => e.Id == addAddressDTO.UserId);

            if(control == null)
            {
                return Ok("User not found");
            }
            else
            {
                addresses.Street = addAddressDTO.Street;
                addresses.Town = addAddressDTO.Town;
                addresses.City = addAddressDTO.City;
                addresses.PostalCode = addAddressDTO.PostalCode;
                addresses.Country = addAddressDTO.Country;
                addresses.FullAddress = addAddressDTO.FullAddress;
                addresses.Latitude = addAddressDTO.Latitude;
                addresses.Longitude = addAddressDTO.Longitude;
                addresses.UserId = addAddressDTO.UserId;
            }

            this.newCafeAutoContext.Add(addresses);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<AddressDTO>(addresses));
            
        }

        [HttpDelete("deleteAddress")]
        public async Task<IActionResult> deleteAddress(int id)
        {
            var address = await newCafeAutoContext.Addresses.FirstOrDefaultAsync(e => e.Id == id);

            if(address == null)
            {
                return BadRequest("Address not found");
            }

            this.newCafeAutoContext.Remove(address);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok("Address deleted");
        }

    }
}
