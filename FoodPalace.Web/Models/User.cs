using FoodPalace.Core.Business;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodPalace.Web.Models
{
    public class User : UserModel, IUser<int>
    {
        public int Id
        {
            get { return UserId; }
            set { UserId = value; }
        }

        public string UserName
        {
            get { return Email; }
            set { Email = value; }
        }

        public User()
        {

        }

        public User(UserModel model)
        {
            this.Assign(model);
        }

    }
}