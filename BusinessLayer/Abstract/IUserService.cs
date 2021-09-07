using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        bool RegisterUser(UserDTO u);
        User LoginUser(UserDTO u);
        bool HasPermission(User sessionuser, string permissionname);

        bool HasPermission(User sessionuser, Permission permission);

    }
}
