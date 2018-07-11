namespace DatingApp.API.Dtos
{
    public class UserForLoginDto
    {
        private string username;
        public string Password { get; set; }

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