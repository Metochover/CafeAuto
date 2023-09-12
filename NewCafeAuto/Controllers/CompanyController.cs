using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewCafeAuto.DTO.CompanyDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;

        public CompanyController(NewCafeAutoContext newCafeAutoContext, IMapper mapper)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
        }

        [HttpPost("signUpCompany")]
        public async Task<IActionResult> signUpCompany(SignUpCompanyDTO signUp)
        {
            DateTime date = DateTime.Now;
            Company company = new Company();

            company.FirstName = signUp.FirstName;
            company.LastName = signUp.LastName;
            company.Username = signUp.Username;
            company.Email = signUp.Email;
            company.Password = signUp.Password;
            company.PhoneNumber = signUp.PhoneNumber;
            company.Photo = signUp.Photo;
            company.Registration = date.ToString("dd.MM.yyyy");

            this.newCafeAutoContext.Add(company);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<CompanyDTO>(company));
        }

        [HttpPost("loginCompany")]
        public async Task<IActionResult> loginCompany(string email, string password)
        {
            var company = await newCafeAutoContext.Company
                .Include(e => e.Cafe).ThenInclude(e => e.Menu)
                .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
            
            if (company == null)
            {
                return BadRequest("Company not found");
            }

            return Ok(mapper.Map<CompanyDTO>(company));
        }
    }
}
