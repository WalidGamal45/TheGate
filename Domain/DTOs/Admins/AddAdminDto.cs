using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Admins
{
    public class AddAdminDto
    {
        [Required(ErrorMessage = "please enter your username")]
        [MaxLength(25, ErrorMessage = "the maxlength is : 25")]
        [MinLength(2, ErrorMessage = "the minlength is : 2")]
        public string Username { get; set; }
        [Required(ErrorMessage = "please enter your password")]
        [MaxLength(25, ErrorMessage = "the maxlength is : 30")]
        [MinLength(5, ErrorMessage = "the minlength is : 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
