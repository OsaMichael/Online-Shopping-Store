using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure.Entities
{
   public class Permission :BaseEntity
    {
        public int PermissionId { get; set; }
        public int PermissionName { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
    }
}
