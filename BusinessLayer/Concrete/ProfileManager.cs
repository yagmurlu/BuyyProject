using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProfileManager : IProfileService
    {
        IProfileDal _profileDal;

        public ProfileManager(IProfileDal profileDal)
        {
            _profileDal = profileDal;
        }

        public Profile GetById(int id)
        {
            return _profileDal.Get(x => x.Id == id);
        }

        public List<Profile> GetList()
        {
            return _profileDal.List();
        }

        public void ProfileAddBL(Profile profile)
        {
            _profileDal.Insert(profile);
        }

        public void ProfileDelete(Profile profile)
        {
            _profileDal.Delete(profile);
        }

        public void ProfileUpdate(Profile profile)
        {
            _profileDal.Update(profile);
        }
    }
}
