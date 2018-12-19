using FoodPalace.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Business
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserModel()
        {

        }
        public UserModel(User user)
        {
            this.Assign(user);
        }
        public User CreateUser(UserModel model)
        {
            return new User()
            {
                Password = model.Password,
                Email = model.Email,

            };
        }

        public User Edit(User entity, UserModel model)
        {
            //entity.UserName = model.UserName;  
            entity.UserId = model.UserId;
            entity.Email = model.Email;
            entity.Password = model.Password;
            return entity;
        }
    }
}
    

