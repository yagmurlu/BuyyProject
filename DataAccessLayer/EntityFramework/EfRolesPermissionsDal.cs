using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using DataAccessLayer.Abstract;

namespace DataAccessLayer.EntityFramework
{
    public class EfRolesPermissionsDal: GenericRepository<RolesPermissions>, IRolesPermissionsDal
    {
    }
}
