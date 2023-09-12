using Microsoft.AspNetCore.Mvc;
using NewCafeAuto.DTO.UserDTO;
using NewCafeAuto.DTO.SignUpUserDTO;
using NewCafeAuto.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NewCafeAuto.Utils;
using NewCafeAuto.DTO.AddressDTO;
using NewCafeAuto.DTO.ProfileDTO;

namespace NewCafeAuto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NewCafeAutoContext newCafeAutoContext;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public AuthController(NewCafeAutoContext newCafeAutoContext, IMapper mapper, IConfiguration configuration)
        {
            this.newCafeAutoContext = newCafeAutoContext;
            this.mapper = mapper;
            this.configuration = configuration; 
        }

        [HttpPost("SignUpUser")]
        public async Task<IActionResult> SignUpUser(SignUpUserDTO signUpUserDTO)
        {
            Users user = new Users();
            Profiles profile = new Profiles();
            DateTime dateTime = DateTime.Now;
           
            user.Username = signUpUserDTO.Username;
            user.Password = signUpUserDTO.Password;
            user.Email = signUpUserDTO.Email;
            user.RegistrationDate = dateTime.ToString("dd.MM.yyyy");
    
            profile.FirstName = signUpUserDTO.FirstName;
            profile.LastName = signUpUserDTO.LastName;
            profile.BirthDate = signUpUserDTO.BirthDate;
            profile.PhoneNumber = signUpUserDTO.PhoneNumber;

            user.Profile = profile;

            this.newCafeAutoContext.Add(profile);
            await newCafeAutoContext.SaveChangesAsync();

            user.ProfileId = profile.Id;
            this.newCafeAutoContext.Add(user);
            await newCafeAutoContext.SaveChangesAsync();

            return Ok(mapper.Map<UserDTO>(user));
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(string email, string password)
        {
            Profiles profiles = new Profiles();

            var user = await newCafeAutoContext.Users
                .Include(x => x.Cards)
                .Include(x => x.Addresses)
                .Include(x => x.Profile)
                .FirstOrDefaultAsync(e => e.Email == email && e.Password == password);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            
            
            return Ok(mapper.Map<UserDTO>(user));
        }

        [HttpGet("showUser")]
        public async Task<IActionResult> showUser(int id)
        {
            var user = await newCafeAutoContext.Users
                .Include(e => e.Addresses)
                .Include(e => e.Profile)
                .Include(e => e.Cards)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(mapper.Map<UserDTO>(user));
        }

        [HttpGet("showProfile")]
        public async Task<IActionResult> showProfile(int id)
        {
            var profile = await newCafeAutoContext.Profiles.FirstOrDefaultAsync(x => x.Id == id);

            if(profile == null)
            {
                return BadRequest("Profile not found");
            }

            return Ok(mapper.Map<ProfileDTO>(profile));
        }



        [HttpPost("RequestPasswordReset")]
        public async Task<IActionResult> RequestPasswordReset(string email)
        {
            JwtCoder jwtCoder = new JwtCoder(configuration, newCafeAutoContext);
            var user = await newCafeAutoContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            string resetToken = jwtCoder.GenerateUniqueToken();
            DateTime tokenExpiration = DateTime.UtcNow.AddMinutes(5);

            user.PasswordResetToken = resetToken;
            user.PasswordResetTokenExpiresAt = tokenExpiration;

            await newCafeAutoContext.SaveChangesAsync();

            jwtCoder.SendResetMail(email, resetToken);

            return Ok("Password reset link sent successfully.");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string resetToken, string newPassword)
        {
            JwtCoder jwtCoder = new JwtCoder(configuration, newCafeAutoContext);
            var user = await newCafeAutoContext.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == resetToken && u.PasswordResetTokenExpiresAt > DateTime.UtcNow);
            if(user == null)
            {
                return BadRequest("Invalid or expired reset token");
            }

            user.Password = jwtCoder.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiresAt = null;

            await newCafeAutoContext.SaveChangesAsync();
            return Ok("Password reset successfully.");

        }

        [HttpPost("RequestEmailReset")]
        public async Task<IActionResult> RequestEmailReset(string email)
        {
            JwtCoder jwtCoder = new JwtCoder(configuration, newCafeAutoContext);
            var user = await newCafeAutoContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            string resetToken = jwtCoder.GenerateUniqueToken();
            DateTime tokenExpiration = DateTime.UtcNow.AddMinutes(5);

            user.EmailProofToken = resetToken;
            user.EmailProofTokenExpiresAt = tokenExpiration;

            await newCafeAutoContext.SaveChangesAsync();

            jwtCoder.SendResetMail(email, resetToken);

            return Ok("Password reset link sent successfully.");
        }

        [HttpPost("ResetEmail")]
        public async Task<IActionResult> ResetEmail(string resetToken, string newEmail)
        {
            JwtCoder jwtCoder = new JwtCoder(configuration, newCafeAutoContext);
            var user = await newCafeAutoContext.Users.FirstOrDefaultAsync(u => u.EmailProofToken == resetToken && u.EmailProofTokenExpiresAt > DateTime.UtcNow);
            if (user == null)
            {
                return BadRequest("Invalid or expired reset token");
            }

            user.Email = jwtCoder.HashPassword(newEmail);
            user.EmailProofToken = null;
            user.EmailProofTokenExpiresAt = null;

            await newCafeAutoContext.SaveChangesAsync();
            return Ok("Email reset successfully.");

        }     
    }
}
