using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProfileService
    {
        List<Profile> GetList();
        void ProfileAddBL(Profile profile);
        Profile GetById(int id);
        void ProfileDelete(Profile profile);
        void ProfileUpdate(Profile profile);
    }
}
