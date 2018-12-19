using FoodPalace.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Business
{
  public  class RoleModel : BaseModel
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; }

        public virtual UserModel User { get; set; }
        public ICollection<PermissionModel> Permissions { get; set; } = new List<PermissionModel>();


        public RoleModel()
        {
            new UserModel();
     
        }

        public RoleModel(Role role)
        {
            this.Assign(role);
            User = new UserModel();
        

        }
        public Role CreateRole(RoleModel model)
        {
            return new Role()
            {
                UserId = model.UserId,
                RoleName = model.RoleName
            };
        }

        public Role Edit(Role entity, RoleModel model)
        {
            entity.RoleId = model.RoleId;
            entity.RoleName = model.RoleName;
            entity.UserId = model.UserId;

            return entity;
        }
    }
}
