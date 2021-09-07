using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(256)]
        public string Password { get; private set; }

        [StringLength(32)]
        public string Username { get; private set; }

        [StringLength(144)]
        public string Email { get; private set; }

        public Address[] Addresses { get; private set; }

        public int ProfileId { get; set; }

        public Profile Profile { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public User(int Id, string Password, string Username, string Email)
        {
            this.Id = Id;
            this.Password = Password;
            this.Username = Username;
            this.Email = Email;
            this.Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public User()
        {

        }

        public void SetId(int id) {
            this.Id = id;
        }

        public void SetUsername(string newUname) {
            //Rulez here
            this.Username = newUname;
        }

        public void SetEmail(string newMail) {
            //Validator here
            this.Email = newMail;
        }

        public string SetPassword(string oldPassword, string newPassword) {
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, this.Password)) return null;
            //Rulez here
            this.Password = this.SetPassword(newPassword);

            return this.Password;
        }

        public string SetPassword(string newPassword)
        {
            //Rulez here
            this.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            return this.Password;
        }

        public bool IsPassword(string passToTest) {
            return BCrypt.Net.BCrypt.Verify(passToTest, this.Password);
        }
      

    }
}
