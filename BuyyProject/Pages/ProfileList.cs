using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuyyProject.Pages
{
    public class ProfileList
    {
        public User Users { get; set; }
        public Profile Profiles { get; set; }
        public Address Addresses { get; set; }
    }
}