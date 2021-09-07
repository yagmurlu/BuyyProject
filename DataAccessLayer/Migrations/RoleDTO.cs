using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace DataAccessLayer.Migrations
{
    public class RoleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string RoleName { get; set; }

        public string Description { get; set; }

        public List<RolesPermissionDTO> Permissions { get; set; }

        public Role ToRole(Context ctx) {
            Role n = new Role();
            n.Id = this.Id;
            n.Name = Name;
            n.RoleName = RoleName;
            n.Description = Description;
            n.RolesPermissions = new List<RolesPermissions>();
            if (this.Permissions.Count > 0) {
                foreach (RolesPermissionDTO p in this.Permissions)
                {
                    try
                    {
                        Permission realP = ctx.Permissions.SingleOrDefault(s => s.PermissionName == p.PermissionName);
                        if (realP != null) {
                            if (realP.Id > 0)
                            {
                                RolesPermissions rp = new RolesPermissions();
                                rp.Permission = realP;
                                rp.PermissionId = realP.Id;
                                rp.Role = n;
                                if (this.Id > 0)
                                {
                                    rp.RoleId = this.Id;
                                }
                                n.RolesPermissions.Add(rp);
                            }
                        }
                        
                        
                    }
                    catch (InvalidOperationException e) {
                        continue;
                    }
                }
            }
            
            return n;
        }
    }
}
