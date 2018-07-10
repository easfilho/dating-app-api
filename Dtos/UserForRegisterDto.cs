using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        private string username;
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The username should have at max 10 characters.")]
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (value != null)
                {
                    this.username = value.ToLower();
                }
            }
        }
    }
}