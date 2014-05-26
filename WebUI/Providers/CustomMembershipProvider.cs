using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using BLL.DomainModel.Entities;
using BLL.DomainModel.Services;
using DI.Infrastructure;

namespace WebUI.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public CustomMembershipProvider()
        {
           userService = (UserService)new NinjectDependencyResolver().GetService(typeof(UserService));
           roleService = (RoleService)new NinjectDependencyResolver().GetService(typeof(RoleService));
        }

        public MembershipUser CreateUser(string email, string password)
        {
            var membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }
            var user = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Password = Crypto.HashPassword(password),
                    CreationDate = DateTime.Now,
                    RoleId = 2
                };

            var role = (from r in roleService.FindAllRoles()
                        where r.Name == "user"
                        select r).FirstOrDefault();

            if (role != null)
            {
                user.RoleId = role.Id;
            }
            userService.SaveUser(user);
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
                                                  bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
                                                             string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = (from u in userService.FindAllUsers()
                        where u.Email == email
                        select u).FirstOrDefault();
            return user != null && Crypto.VerifyHashedPassword(user.Password, password);//&& user.Password == password;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = (from u in userService.FindAllUsers()
                        where u.Email == email
                        select u).FirstOrDefault();

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Email, null, null, null, null,
                false, false, user.CreationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}