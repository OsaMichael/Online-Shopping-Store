using FoodPalace.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Business
{
   public class PermissionModel : BaseModel
    {
        public int PermissionId { get; set; }
        public int PermissionName { get; set; }

        public List<RolePermissionModel> RolePermissions { get; set; } = new List<RolePermissionModel>();

        public PermissionModel()
        {
        }
        public PermissionModel(Permission permission)
        {
            this.Assign(permission);
        }

        public Permission Create(PermissionModel model)
        {
            return new Permission
            {
                PermissionName = model.PermissionName,
                CreatedBy = model.CreatedBy
            };
        }
        public Permission Edit(Permission entity, PermissionModel model)
        {
            entity.PermissionName = model.PermissionName;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = DateTime.Now;
            return entity;
        }
    }
}
