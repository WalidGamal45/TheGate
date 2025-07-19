using System.ComponentModel.DataAnnotations;

namespace Domain.Domains
{
    public class Admin
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="please enter your username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
