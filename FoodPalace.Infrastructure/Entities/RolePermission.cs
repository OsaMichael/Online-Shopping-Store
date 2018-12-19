﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure.Entities
{
  public  class RolePermission : BaseEntity
    {
          public int RolePermissionId { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }


    public virtual Permission Permission { get; set; }
    public virtual Role Role { get; set; }
}
}
