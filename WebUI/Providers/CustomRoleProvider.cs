using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using BLL.DomainModel.Entities;
using BLL.DomainModel.Services;
using DI.Infrastructure;

namespace WebUI.Providers
{
    public class CustomRoleProvider: RoleProvider
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public CustomRoleProvider()
        {
           userService = (UserService)new NinjectDependencyResolver().GetService(typeof(UserService));
           roleService = (RoleService)new NinjectDependencyResolver().GetService(typeof(RoleService));
        }
        public override bool IsUserInRole(string email, string roleName)
        {
            var user =  (from u in userService.FindAllUsers()
                             where u.Email == email
                            select u).FirstOrDefault();
            if (user == null) return false;

            var userRole = roleService.FindAllRoles().First(r => r.Id == user.RoleId);
            return userRole != null && userRole.Name == roleName;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };
            var user = userService.FindAllUsers().FirstOrDefault(u => u.Email == email);
            if (user == null) return roles;

            var userRole = roleService.FindAllRoles().First(r => r.Id == user.RoleId);
            if (userRole != null)
            {
                roles = new[] { userRole.Name };
            }
            return roles;
        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() { Name = roleName };
            roleService.SaveRoles(new List<RoleEntity>{newRole});
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}