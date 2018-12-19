using FoodPalace.Core.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FoodPalace.Web.App_Start.Ninject;

namespace FoodPalace.Web.Modules.Security
{
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private string[] _permissions;
        public AuthorizeUserAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //First Make Sure that the User is Authenticated
            if (httpContext.User.Identity.IsAuthenticated)
            {
                //Get Permissions List in Session
                var permissions = httpContext.Session[SessionKeys.Permissions] as string[];
                if (permissions == null || !permissions.Any())
                {
                    using (var resolver = NinjectContainer.ResolutionScope())
                    {
                        var _user = resolver.Get<IUserManager>();
                        //var getPermissions = _user.GetPermissions(httpContext.User.Identity.Name);
                        //if (getPermissions.Succeeded)
                        //{
                        //    var query = from permission in _permissions
                        //                    //join userpermission in getPermissions.Result
                        //                join rolePermission in getPermissions.Result
                        //                //on permission.ToLower() equals userpermission.Name.ToLower()
                        //                on permission.ToLower() equals rolePermission.PermissionName.ToString()
                        //                select permission;
                        //    httpContext.Session[SessionKeys.Permissions] = getPermissions.Result.Select(p => p.PermissionName).ToArray();
                        //    return query.Any();
                        //}
                    }
                }
                else
                {
                    var query = from permission in _permissions
                                join userpermission in permissions
                                on permission.ToLower() equals userpermission.ToLower()
                                select permission;
                    return query.Any();
                }
            }
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("/Home/NotAuthorized");
        }
    }
}