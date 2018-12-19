using FoodPalace.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Interface
{
    public interface IUserManager
    {
        Operation CreateUser(UserModel model);
        Operation<UserModel[]> GetUsers();
        Operation<UserModel> GetUserById(int userId);
        Operation<UserModel> UpdateUser(UserModel model);
        Operation ChangePassword(string mail, string password, string newpassword);
        Operation<UserModel> ResetPassword(string email);
        Operation<UserModel> RecoverPassword(string email);
        Operation<bool> ValidateUser(string userName, string password);


        Operation<RoleModel[]> RemoveRolesFromUser(int userId, string[] roles, string createdBy);
        Operation CreateRegister(UserModel model);
        Operation<RoleModel[]> AddRolesToUser(int userId, string[] roles, long createdBy);


        Operation CreateRole(RoleModel model);
        Operation<RoleModel[]> GetRoles();
        Operation<RoleModel> GetRoleById(int roleId);
        Operation<RoleModel> UpdateRole(RoleModel model);
        Operation<RoleModel[]> GetRoles(int userId);
        void AssignRole(int userId, int roleId);
        Operation<RoleModel> RemoveRoleFromUser(int userId, string roleName);
        Operation<bool> HasRole(string email, string roleName);
        Operation<RoleModel[]> GetUserRoleByUserId(int userId);
        Operation<RoleModel[]> GetUnAssignedUserRoleByUserId(int userId);
        #region Permission
        Operation<bool> HasPermission(int userId, string permission);
        Operation CreatePermission(PermissionModel model);
        Operation<PermissionModel[]> GetPermissions();
        //Operation<PermissionModel[]> GetPermission();
        Operation<PermissionModel> GetPermissionById(int permissionId);
        Operation<PermissionModel> UpdatePermission(PermissionModel model);
        //Operation<PermissionModel[]> GetPermissions(string userName);
        Operation<PermissionModel[]> GetUnAssignedPermissionsByRoleId(int roleId);
        Operation<PermissionModel[]> GetRolePermissionsByRoleId(int roleId);
        Operation<PermissionModel[]> RemovePermissionsFromRole(int roleId, string[] permissions, string modifiedBy);
        Operation<PermissionModel[]> AddPermissionsToRole(int roleId, string[] roles, long createdBy);
        #endregion
    }

}

