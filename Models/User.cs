namespace DatingApp.API.Models
{
    public class User
    {
        public User()
        {

        }

        public User(string Username, byte[] PasswordHash, byte[] PasswordSalt)
        {
            this.Username = Username;
            this.PasswordHash = PasswordHash;
            this.PasswordSalt = PasswordSalt;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}