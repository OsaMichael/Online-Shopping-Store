using FoodPalace.Infrastructure.Intaface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Infrastructure
{
  public  class FormsAuthenticationProvider : IAuthentication
    {
        private readonly DataContext context = new DataContext();



        public bool Authenticate(string username, string password)
        {
            var result = context.Users.FirstOrDefault(u => u.UserId.ToString() == username && u.Password == password);
            if (result == null)
                return false;
            return true;
        }


        public bool Logout()
        {
            return true;
        }
    }
}
