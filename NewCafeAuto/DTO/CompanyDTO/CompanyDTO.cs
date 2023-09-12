using NewCafeAuto.DTO.CafeDTO;
using NewCafeAuto.Models;

namespace NewCafeAuto.DTO.CompanyDTO
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Photo { get; set; }
        public string Registration { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiresAt { get; set; }
        public string EmailProofToken { get; set; }
        public DateTime? EmailProofTokenExpiresAt { get; set; }
        public string SignToken { get; set; }
        public bool? IsLogin { get; set; }
        public virtual ICollection<CafeDTO.CafeDTO> Cafe { get; set; }
    }
}
