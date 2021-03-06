﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure.Entities
{
  public  class BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Today;
        }
    }
}
