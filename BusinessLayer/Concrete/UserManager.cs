using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Unity;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _dal = new EfUserDal();
        IMailService mailService = new MailManager();
        IRoleDal _role = new EfRoleDal();
        IPermissionDal _permission = new EfPermissionDal();
        IRolesPermissionsDal _rpdal = new EfRolesPermissionsDal();
        IProfileDal _profile = new EfProfileDal();

        public UserManager()
        {
        }

        public User LoginUser(UserDTO u)
        {
            User a = _dal.Get(x => x.Username == u.Username);
            if (a == null) return null;
            if (a.IsPassword(u.Password))
            {
                return a;
            }
            else {
                return null;
            }

        }

        public bool RegisterUser(UserDTO u)
        {
            User _tobecreated = new User();
            _tobecreated.SetUsername(u.Username);
            _tobecreated.SetPassword(u.Password);
            _tobecreated.SetEmail(u.Email);
            _tobecreated.RoleId = _role.Get(x => x.RoleName == "app:user").Id;
            Profile newUserProfile = new Profile();
            newUserProfile.SetName(u.Name);
            newUserProfile.SetSurname(u.Surname);
            newUserProfile.SetUser(_tobecreated);
            _tobecreated.Profile = newUserProfile;
            _dal.Insert(_tobecreated);

            MailMessageDTO m = new MailMessageDTO();
            m.Body = "Hi, " + u.Username+"!<br><br>Welcome to BUYY.com";
            m.To = u.Email;
            m.Title = "Welcome to BUYY.com!";
            m.Summary = "Welcome to BUYY.com! We're happy to see you here.";
            mailService.SendEmail(m);

            return true;
        }

        public bool HasPermission(User sessionuser, string permissionname)
        {
            Permission p = _permission.Get(x => x.PermissionName == permissionname);
            return this.HasPermission(sessionuser, p);
        }

        public bool HasPermission(User sessionuser, Permission permission) {
            if (sessionuser != null) {
                User ondb = _dal.Get(x => x.Id == sessionuser.Id);
                Role userrole = _role.Get(x => x.Id == sessionuser.RoleId);
                if (userrole != null) {
                    RolesPermissions[] rp = _rpdal.List(x => x.RoleId == userrole.Id).ToArray();
                    foreach (RolesPermissions rps in rp) {
                        Permission _tmp = _permission.Get(x => x.Id == rps.PermissionId);
                        if (_tmp.PermissionName == permission.PermissionName) return true;
                    }
                }
                    
            }
            
            return false;
        }
    }
}
