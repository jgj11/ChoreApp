using System.ComponentModel.DataAnnotations;

namespace ChoreApp.API.Dtos
{
    public class UserForRegisterDto
    {
        //validating here
        [Required]
        public string Username { get; set; }


        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 10 characters dude")]
        public string Password { get; set; }
    }
}