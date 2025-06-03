using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Domain.User
{
    public class UserError
    {
        public static Error UserNotFound = Error.NotFound
            (code: "user.not.found" , description: "User Not Found");

        public static Error NameNotValid = Error.Validation
            (code: "name.not.valid", description: "Entered Name is Not Valid");

        public static Error EmailNotValid = Error.Validation
            (code: "email.not.valid", description: "Entered Email is Not Valid");

        public static Error EmailAlreadyTaken = Error.Validation
            (code: "email.already.taken", description: "Email is Already Taken");

        public static Error SessionAlreadyExists = Error.Validation
            (code: "session.already.exists", description: "User Already Has This Session");

        public static Error SessionNotExists = Error.NotFound
            (code: "session.not.exists", description: "User Doesn't Have This Session");

        public static Error SessionMemberAlreadyExists = Error.Validation
            (code: "sessionMember.already.exists", description: "User is Already A Member of The Session");

        public static Error SessionMemberNotExists = Error.NotFound
            (code: "sessionMember.not.exists", description: "User is Not A Member of The Session");

        public static Error TeamMemberAlreadyExists = Error.Validation
            (code: "teamMember.already.exists", description: "User is Already A Member of The Team");

        public static Error TeamMemberNotExists = Error.NotFound
            (code: "teamMember.not.exists", description: "User is Not A Member of The Team");

        public static Error ProjectMemberAlreadyExists = Error.Validation
            (code: "projectMember.already.exists", description: "User is Already A Member of The Project");

        public static Error ProjectMemberNotExists = Error.NotFound
            (code: "projectMember.not.exists", description: "User is Not A Member of The Project");

        public static Error TaskAlreadyAssigned = Error.Validation
            (code: "task.already.assigned", description: "Task Already Assigned To The User");

        public static Error TaskNotAssigned = Error.Validation
            (code: "task.not.assigned", description: "Task Is Not Assigned To The User");

        public static Error PasswordEightChar = Error.Validation
            (code: "password.8.char", description: "Password must be at least 8 characters");

        public static Error PasswordContainLetter = Error.Validation
            (code: "password.contain.letter", description: "Password must contain at least one letter");

        public static Error PasswordContainNumber = Error.Validation
            (code: "password.contain.number", description: "Password must contain at least one number");

        public static Error EmailOrPasswordNotCorrect = Error.Validation
            (code: "emailOrPassword.not.correct", description: "Email Or Password Is Not Correct");
    }
}