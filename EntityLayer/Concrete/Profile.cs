using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EntityLayer.Concrete
{
    public class Profile
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        [Required]
        public User User { get; private set; }

        public void SetUser(User u) {
            this.User = u;        }

        public void SetId(int id) {
            this.Id = id;
        }

        public void SetName(string name) {
            //Checker here
            this.Name = name;
        }

        public void SetSurname(string surname) {
            //Checker here
            this.Surname = surname;
        }

    }
}
