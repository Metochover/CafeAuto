using Microsoft.AspNetCore.Identity;
using NewCafeAuto.DTO.AddressDTO;
using NewCafeAuto.DTO.CardDTO;
using NewCafeAuto.Models;
using NewCafeAuto.DTO.CardDTO;

namespace NewCafeAuto.DTO.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiresAt { get; set; }
        public string EmailProofToken { get; set; }
        public DateTime? EmailProofTokenExpiresAt { get; set; }
        public string SignToken { get; set; }
        public bool? IsLogin { get; set; }
        public int? ProfileId { get; set; }
        public virtual ICollection<UserAddressDTO> Addresses { get; set; }
        public virtual ICollection<CardDTO.CardDTO> Cards { get; set; }
        public virtual UserProfileDTO Profile { get; set; }

    }
}
