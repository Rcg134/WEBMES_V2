using System.ComponentModel.DataAnnotations;

namespace WEBMES_V2.Models.DTO.LoginUserDTO
{
    public class UserDTO
    {
        public long UserCode { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        public bool? Active { get; set; }
    }
}
