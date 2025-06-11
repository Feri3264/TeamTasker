using ErrorOr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain.Common;

namespace Tasker.Domain.User
{
    public class UserModel : BaseModel
    {
        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string RefreshToken { get; private set; }

        public DateTime TokenExpire { get; private set; }

        public bool IsDelete { get; private set; }


        //navigation
        private readonly List<Guid> _taskIds = new(); //set
        public IReadOnlyList<Guid> TaskIds => _taskIds; //get


        //ctor
        private UserModel(
            string _name,
            string _email,
            string _password,
            string _refreshToken,
            DateTime _tokenExpire)
        {
            Id = Guid.NewGuid();
            Name = _name;
            Email = _email;
            Password = _password;
            RefreshToken = _refreshToken;
            TokenExpire = _tokenExpire;
        }

        //methods
        public static ErrorOr<UserModel> Create(
            string _name,
            string _email,
            string _password,
            string _refreshToekn,
            DateTime _tokenExpire)
        {
            //password validation
            var validatePassword = ValidatePassword(_password);
            if (validatePassword.IsError)
                return validatePassword.Errors;

            //name validation
            if (string.IsNullOrWhiteSpace(_name))
                return UserError.UserNotFound;

            //email validation
            var verifyEmail = ValidateEmail(_email);
            if (verifyEmail.IsError)
                return verifyEmail.Errors;

            return new UserModel(_name, _email, _password , _refreshToekn , _tokenExpire);
        }

        public ErrorOr<Success> SetName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return UserError.NameNotValid;

            Name = value;
            return Result.Success;
        }

        public ErrorOr<Success> SetEmail(string value)
        {
            var verifyEmail = ValidateEmail(value);
            if (verifyEmail.IsError)
                return verifyEmail.Errors;

            Email = value;
            return Result.Success;
        }

        public ErrorOr<Success> SetPassword(string value)
        {
            var validate = ValidatePassword(value);
            if (validate.IsError)
                return validate.Errors;

            Password = value;
            return Result.Success;
        }

        public void SetRefreshToken(string value)
        {
            RefreshToken = value;
        }

        public void SetTokenExpireTime(DateTime date)
        {
            TokenExpire = date;
        }

        public ErrorOr<Success> AddTask(Guid taskId)
        {
            if (_taskIds.Contains(taskId))
                return UserError.TaskAlreadyAssigned;

            _taskIds.Add(taskId);
            return Result.Success;
        }

        public ErrorOr<Success> RemoveTask(Guid taskId)
        {
            if (_taskIds.Count == 0 || !_taskIds.Contains(taskId))
                return UserError.TaskNotAssigned;

            _taskIds.Remove(taskId);
            return Result.Success;
        }

        public void Delete()
        {
            IsDelete = !IsDelete;
        }

        #region Password Validation
        private static ErrorOr<Success> ValidatePassword(string password)
        {
            if (password.Length <= 8)
            {
                return UserError.PasswordEightChar;
            }

            if (!password.Any(c => IsLetter(c)))
            {
                return UserError.PasswordContainLetter;
            }

            if (!password.Any(c => IsDeigit(c)))
            {
                return UserError.PasswordContainNumber;
            }

            return Result.Success;
        }
        private static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        private static bool IsDeigit(char c)
        {
            return (c >= '0' && c <= '9');
        }


        #endregion

        #region Email Validation
        private static ErrorOr<Success> ValidateEmail(string email)
        {
            var verifyEmail = new EmailAddressAttribute();
            if (!verifyEmail.IsValid(email))
            {
                return UserError.EmailNotValid;
            }

            return Result.Success;
        }
        #endregion

        private UserModel() { }
    }
}