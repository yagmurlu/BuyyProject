using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class Address
    {
        [Key]
        public int AddressId { get; private set; }
        
        public string City { get; private set; }

        public string Country { get; private set; }

        public string Description { get; private set; }

        public int Priority { get; set; }
        
        public int UserId { get; private set; }
        public User User { get; private set; }

        public void SetCountry(string country) {
            //Check maps
            this.Country = country;
        }

        public void SetCity(string city) {
            //Check maps
            this.City = city;
        }

        public void SetAddress(string address) {
            this.Description = address;
        }

        public void SetUser(User u) { 
            this.User = u;
            this.UserId = u.Id;
        }
    }
}
