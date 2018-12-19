using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure.Entities
{
   public partial class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
   


        public Role()
        {
          
        }
    }
}
