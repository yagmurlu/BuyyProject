using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class RolesPermissions
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Required]
        public Role Role { get; set; }

        public int PermissionId { get; set; }

        [Required]
        public Permission Permission { get; set; }
    }
}
