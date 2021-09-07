using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace DataAccessLayer.Migrations
{
    [Serializable]
    public class RolePermissionDTO
    {
        public List<Permission> Permissions { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}
