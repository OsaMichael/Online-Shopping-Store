using FoodPalace.Core.Business;
using FoodPalace.Core.Interface;
using FoodPalace.Infrastructure.Entities;
using FoodPalace.Infrastructure.Intaface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPalace.Core.Manager
{
  public  class UserManager : IUserManager
    {
        private IDataRepository _db;

        public UserManager(IDataRepository db)
        {
            _db = db;
        }
        public Operation CreateUser(UserModel model)
        {
            return Operation.Create(() =>
            {
                var userEntity = _db.Query<User>().Where(u => u.Email == model.Email).FirstOrDefault();
                if (userEntity != null) throw new Exception("User already exist");
                var entity = model.CreateUser(model);
                _db.Add(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return model;
            });
        }
        public Operation<UserModel[]> GetUsers()
        {
            return Operation.Create(() =>
            {
                var entities = _db.Query<User>().ToList();
                var models = entities.Select(u => new UserModel(u)).ToArray();
                return models;
            });
        }
        public Operation<UserModel> GetUserById(int userId)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.UserId == userId).FirstOrDefault();
                if (user == null) throw new Exception("User does not exist");

                var model = new UserModel(user);
                return model;
            });
        }
        public Operation CreateRegister(UserModel model)
        {
            return Operation.Create(() =>
            {
                //using (DataContext db = new DataContext())
                //{
                var userd = _db.Query<User>().Where(u => u.Email == model.Email).FirstOrDefault();
                if (userd != null) throw new Exception("User exist");
                var rrr = model.CreateUser(model);
                _db.Add(rrr);
                _db.SaveChanges();
                //}

                return model;
            });
        }

        public Operation<UserModel> UpdateUser(UserModel model)
        {
            return Operation.Create(() =>
            {
                //model.Validate();

                //var user = _db.Query<User>().Where(u => u.UserId == model.UserId).FirstOrDefault();
                var user = _db.Query<User>().Where(u => u.UserId == model.UserId).FirstOrDefault();

                if (user == null) throw new Exception("User does not exist");

                var entity = model.Edit(user, model);
                _db.Update(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return new UserModel(entity);
            });
        }
        public Operation ChangePassword(string mail, string password, string newpassword)
        {
            return Operation.Create(() =>
            {
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(newpassword)) throw new Exception("Password cannot be empty");
                if (newpassword.Length < 8) throw new Exception("Password should be at least 8 characters");

                //Get User
                var user = _db.Query<User>().Where(u => u.Email == mail).FirstOrDefault();
                if (user == null) throw new Exception("Invalid Username/Email");
                //if (user.Password != _hasher.ComputeHash(password, null)) throw new Exception("Invalid Password");

                //user.Password = _hasher.ComputeHash(newpassword, null);
                UpdateUser(new UserModel(user)).Throw();
            });
        }
        public Operation<UserModel> ResetPassword(string email)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.Email == email).FirstOrDefault();
                if (user == null) throw new Exception("Invalid Email Address");

                //Generate Password and Encrypt it

                //string newpass = RandomAlpha(8).ToUpper();
                //string encPassword = _hasher.ComputeHash(newpass, null);

                //Set new Password and Return it
                //user.Password = encPassword;
                //user.Password = newpass;
                return UpdateUser(new UserModel(user)).Throw();
            });
        }
        public Operation<UserModel> RecoverPassword(string email)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.Email == email).FirstOrDefault();
                if (user == null) throw new Exception("Invalid Email Address");

                //Generate Password and Encrypt it

                //string newpass = RandomAlpha(8).ToUpper();
                //string encPassword = _hasher.ComputeHash(newpass, null);

                //Set new Password and Return it
                //user.Password = encPassword;
                //user.Password = newpass;
                return UpdateUser(new UserModel(user)).Throw();
            });
        }

        public Operation<bool> ValidateUser(string userName, string password)
        {
            return Operation.Create(() =>
            {

                var isExist = _db.Query<User>().Where(u => u.Email == userName && u.Password == password).FirstOrDefault();
                if (isExist == null) throw new Exception("User does not exist");

                return true;
            });
        }

        public Operation CreateRole(RoleModel model)
        {
            return Operation.Create(() =>
            {

                var roleEntity = _db.Query<Role>().Where(r => r.RoleName == model.RoleName).FirstOrDefault();
                if (roleEntity != null) throw new Exception("role already exist");
                var entity = model.CreateRole(model);
                _db.Add(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return model;
            });
        }

        public Operation<RoleModel[]> GetRoles()
        {
            return Operation.Create(() =>
            {
                var entities = _db.Query<Role>().ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }
        public Operation<RoleModel> GetRoleById(int roleId)
        {
            return Operation.Create(() =>
            {
                var entity = _db.Query<Role>().Where(r => r.RoleId == roleId).FirstOrDefault();
                var model = new RoleModel(entity);
                return model;
            });
        }
        public Operation<RoleModel> UpdateRole(RoleModel model)
        {
            return Operation.Create(() =>
            {
                model.Validate();

                var role = _db.Query<Role>().Where(r => r.RoleId == model.RoleId).FirstOrDefault();

                if (role == null) throw new Exception("Role does not exist");

                var entity = model.Edit(role, model);
                _db.Update(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return new RoleModel(entity);
            });
        }
        public Operation<RoleModel[]> GetRoles(int userId)
        {
            return Operation.Create(() =>
            {
                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var roles = _db.Query<Role>().Where(r => roleIds.Contains(r.RoleId)).ToList();
                var models = roles.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }
        //since i m passing only two values of the usres ids or otherwise string if its names// 
        //if i have more than two values i can pass it using model or creating a model for it
        public void AssignRole(int userId, int roleId)
        {
            //return Operation.Create(() =>
            //{
            //    var role = _db.Query<Role>().FirstOrDefault(r => r.RoleName == roleName);
            //    if (role == null) throw new Exception("Invalid Role");

            //    var user = _db.Query<User>().FirstOrDefault(u => u.UserId == userId);
            //    if (user == null) throw new Exception("Invalid User ID");

            //    var userroles = _db.Query<UserRole>(ur => ur.Role, ur => ur.User).Where(ur => ur.UserId == userId).ToList();

            //    var userRoleExists = userroles.Select(r => r.RoleId).Contains(role.RoleId);

            //    if (userRoleExists) throw new Exception($"User already has role: {role.RoleName}");

            //var assignRole = AssignRole(userId, role.Name);
            var user = new UserRole
            {
                UserId = userId,
                RoleId = roleId,
            };

            _db.Add(user);
            _db.SaveChanges().Throw();

            // });
        }
        public Operation<RoleModel[]> AddRolesToUser(int userId, string[] roles, long createdBy)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.UserId == userId).FirstOrDefault();
                if (user == null) throw new Exception("User does not exist");
                foreach (var role in roles)
                {
                    if (string.IsNullOrEmpty(role)) throw new Exception($"{role} does not exist");

                    int roleId = int.Parse(role);
                    var userRole = _db.Query<UserRole>().Where(ur => ur.UserId == userId && ur.RoleId == roleId);

                    if (userRole.Any()) continue;
                    var entity = new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        //CreatedBy = createdBy,
                        //CreatedDate = DateTime.Now
                    };
                    _db.Add(entity);
                }
                var result = _db.SaveChanges();
                if (result.Succeeded == false) throw new Exception(result.Message);

                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var entities = _db.Query<Role>().Where(r => roleIds.Contains(r.RoleId)).ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }

        public Operation<RoleModel> RemoveRoleFromUser(int userId, string roleName)
        {
            return Operation.Create(() =>
            {
                //Get User's Role 
                var userRoles = _db.Query<UserRole>(ur => ur.User, ur => ur.Role)
                     .Where(_ur => _ur.User.UserId == userId)
                     .ToArray();

                //Get Role to delete
                var usrrole = userRoles.Where(ur => string.Equals(ur.Role.RoleName, roleName, StringComparison.OrdinalIgnoreCase))
                                 .FirstOrDefault();


                if (usrrole == null) throw new Exception("User does not have this Role");

                var model = new RoleModel(usrrole.Role);
                _db.Delete(usrrole);
                _db.SaveChanges();
                return model;
            });
        }


        public Operation<bool> HasRole(string email, string roleName)
        {
            return Operation.Create(() =>
            {
                return _db.Query<UserRole>(_ur => _ur.Role, _ur => _ur.User)
                      .Where(ur => ur.User.Email == email)
                      .Where(ur => ur.Role.RoleName == roleName)
                      .Any();
            });
        }
        public Operation<RoleModel[]> GetUserRoleByUserId(int userId)
        {
            return Operation.Create(() =>
            {
                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var entities = _db.Query<Role>().Where(r => roleIds.Contains(r.RoleId)).ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }

        public Operation<RoleModel[]> GetUnAssignedUserRoleByUserId(int userId)
        {
            return Operation.Create(() =>
            {
                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var entities = _db.Query<Role>().Where(r => !roleIds.Contains(r.RoleId)).ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }
        public Operation<RoleModel[]> AddRolesToUser(int userId, string[] roles, string createdBy)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.UserId == userId).FirstOrDefault();
                if (user == null) throw new Exception("User does not exist");
                foreach (var role in roles)
                {
                    if (!string.IsNullOrEmpty(role)) throw new Exception($"{role} does not exist");

                    int roleId = int.Parse(role);
                    var userRole = _db.Query<UserRole>().Where(ur => ur.UserId == userId && ur.RoleId == roleId);

                    if (userRole.Any()) continue;
                    var entity = new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        //CreatedBy = createdBy,
                        //CreatedDate = DateTime.Now
                    };
                    _db.Add(entity);
                }
                var result = _db.SaveChanges();
                if (result.Succeeded == false) throw new Exception(result.Message);

                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var entities = _db.Query<Role>().Where(r => roleIds.Contains(r.RoleId)).ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }

        public Operation<RoleModel[]> RemoveRolesFromUser(int userId, string[] roles, string createdBy)
        {
            return Operation.Create(() =>
            {
                var user = _db.Query<User>().Where(u => u.UserId == userId).FirstOrDefault();
                if (user == null) throw new Exception("User does not exist");
                foreach (var role in roles)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        int roleId = int.Parse(role);
                        var userRole = _db.Query<UserRole>().Where(ur => ur.UserId == userId && ur.RoleId == roleId).FirstOrDefault();

                        _db.Delete(userRole);
                    }
                }
                var result = _db.SaveChanges();
                if (result.Succeeded == false) throw new Exception(result.Message);

                var roleIds = _db.Query<UserRole>().Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();
                var entities = _db.Query<Role>().Where(r => roleIds.Contains(r.RoleId)).ToList();
                var models = entities.Select(r => new RoleModel(r)).ToArray();
                return models;
            });
        }

        public Operation<bool> HasPermission(int userId, string permission)
        {
            return Operation.Create(() =>
            {

                var uroles = _db.Query<UserRole>(_r => _r.User, _r => _r.Role)
                    .Where(_ur => _ur.UserId == userId).ToList().Select(_ur => _ur.Role.RoleName)
                    .ToList();

                //return _db.Query<RolePermission>(_rp => _rp.Role, _rp => _rp.Permission)
                //            .Where(_rp =>  uroles.Contains(_rp.Permission.PermissionName))
                //            .Where(_rp => _rp.Permission.PermissionName == permission)
                //            .Any();

                var bb = _db.Query<RolePermission>(_rp => _rp.Role, _rp => _rp.Permission)
                          .Where(_rp => uroles.Contains(_rp.Permission.PermissionName.ToString()))
                          .Where(_rp => _rp.Permission.PermissionName.ToString() == permission)
                          .Any();

                return bb;
            });
        }
        public Operation<PermissionModel[]> GetPermissions()
        {
            return Operation.Create(() =>
            {
                var entities = _db.Query<Permission>().ToList();
                var models = entities.Select(p => new PermissionModel(p)).ToArray();
                return models;
            });
        }


        public Operation CreatePermission(PermissionModel model)
        {
            return Operation.Create(() =>
            {
                var permissionEntity = _db.Query<Permission>().Where(p => p.PermissionId == model.PermissionId).FirstOrDefault();
                if (permissionEntity != null) throw new Exception("Permission already exist");
                var entity = model.Create(model);
                _db.Add(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return model;
            });
        }

        public Operation<PermissionModel> GetPermissionById(int permissionId)
        {
            return Operation.Create(() =>
            {
                var entity = _db.Query<Permission>().Where(p => p.PermissionId == permissionId).FirstOrDefault();
                var model = new PermissionModel(entity);
                return model;
            });
        }
        public Operation<PermissionModel> UpdatePermission(PermissionModel model)
        {
            return Operation.Create(() =>
            {
                model.Validate();

                var permission = _db.Query<Permission>().Where(r => r.PermissionId == model.PermissionId).FirstOrDefault();

                if (permission == null) throw new Exception("Permission does not exist");

                var entity = model.Edit(permission, model);
                _db.Update(entity);
                var result = _db.SaveChanges();
                if (result.Succeeded == false)
                {
                    throw new Exception(result.Message);
                }
                return new PermissionModel(entity);
            });
        }

        //public Operation<PermissionModel[]> GetPermissions(string username)
        //{
        //    return Operation.Create(() =>
        //    {
        //        //  int userID = int.Parse(userid);

        //        var user = _db.Query<User>().Where(u => u.Name == username).FirstOrDefault();
        //        var roleids = _db.Query<UserRole>().Where(c => c.UserId == user.UserId).Select(ur => ur.RoleId).ToList();
        //        var roles = _db.Query<Role>().Where(r => roleids.Contains(r.RoleId)).ToList();
        //        var permissionids = _db.Query<RolePermission>().Where(p => roleids.Contains(p.RoleId)).Select(p => p.PermissionId).ToList();
        //        var permissions = _db.Query<Permission>().Where(p => permissionids.Contains(p.PermissionId)).OrderBy(c => c.PermissionName)
        //                      .Select(c => new PermissionModel
        //                      {
        //                          PermissionName = c.PermissionName
        //                      }).ToArray();
        //        return permissions;
        //    });
        //}

        public Operation<PermissionModel[]> GetUnAssignedPermissionsByRoleId(int roleId)
        {
            return Operation.Create(() =>
            {
                var permissionIds = _db.Query<RolePermission>().Where(r => r.RoleId == roleId).Select(rp => rp.PermissionId).ToList();
                //var unassignedPerms = _db.Query<Permission>().Where(p => !assignedPerms.Any(es => (es.PermissionId == p.PermissionId))).ToList()
                var entities = new List<Permission>();
                if (permissionIds == null)
                    entities = _db.Query<Permission>().ToList();
                else
                    entities = _db.Query<Permission>().Where(p => !permissionIds.Contains(p.PermissionId)).ToList();
                var models = entities.Select(p => new PermissionModel
                {
                    PermissionId = p.PermissionId,
                    PermissionName = p.PermissionName
                }).ToArray();
                return models;
            });

        }

        public Operation<PermissionModel[]> GetRolePermissionsByRoleId(int roleId)
        {
            return Operation.Create(() =>
            {
                var permissionids = _db.Query<RolePermission>().Where(r => r.RoleId == roleId).Select(p => p.PermissionId).ToList();
                var assignedPerms = _db.Query<Permission>().Where(p => permissionids.Contains(p.PermissionId)).OrderBy(c => c.PermissionName)
                  .Select(p => new PermissionModel
                  {
                      PermissionId = p.PermissionId,
                      PermissionName = p.PermissionName
                  }).ToArray();

                return assignedPerms;
            });

        }

        public Operation<PermissionModel[]> RemovePermissionsFromRole(int roleId, string[] permissions, string modifiedBy)
        {
            return Operation.Create(() =>
            {
                foreach (var permission in permissions)
                {
                    if (!string.IsNullOrEmpty(permission))
                    {
                        var permissionId = int.Parse(permission);
                        var rolepermission = _db.Query<RolePermission>().Where(p => p.PermissionId == permissionId && p.RoleId == roleId).FirstOrDefault();

                        _db.Delete(rolepermission);
                    }
                }
                var result = _db.SaveChanges();
                if (result.Succeeded == false) throw new Exception(result.Message);
                var permissionIds = _db.Query<RolePermission>().Where(p => p.RoleId == roleId).Select(rp => rp.PermissionId).ToList();
                var entities = _db.Query<Permission>().Where(p => permissionIds.Contains(p.PermissionId)).ToList();
                var models = entities.Select(rp => new PermissionModel(rp)).ToArray();
                return models;
            });
        }

        public Operation<PermissionModel[]> AddPermissionsToRole(int roleId, string[] roles, long createdBy)
        {
            return Operation.Create(() =>
            {
                foreach (var permission in roles)
                {
                    if (!string.IsNullOrEmpty(permission))
                    {
                        int permissionId = int.Parse(permission);
                        var ifExist = _db.Query<RolePermission>().Where(p => p.PermissionId == permissionId && p.RoleId == roleId);

                        if (ifExist.Any()) continue;
                        var entity = new RolePermission
                        {
                            RoleId = roleId,
                            PermissionId = permissionId,
                            CreatedBy = createdBy,
                            CreatedDate = DateTime.Now
                        };
                        _db.Add(entity);
                    }
                }
                var result = _db.SaveChanges();
                if (result.Succeeded == false) throw new Exception(result.Message);
                var permissionIds = _db.Query<RolePermission>().Where(p => p.RoleId == roleId).Select(rp => rp.PermissionId).ToList();
                var entities = _db.Query<Permission>().Where(p => permissionIds.Contains(p.PermissionId)).ToList();
                var models = entities.Select(rp => new PermissionModel(rp)).ToArray();
                return models;
            });
        }


    }
}
