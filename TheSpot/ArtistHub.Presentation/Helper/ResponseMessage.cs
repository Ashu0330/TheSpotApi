using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFPLSkillUp.Presentation.Helper
{
    public enum ResponseMessage
    {
        /// <summary>
        /// The ok
        /// </summary>
        [Description("Success")]
        Ok = 200,

        /// <summary>
        /// The validation failed
        /// </summary>
        [Description("Validation failed")]
        ValidationFailed = 201,

        /// <summary>
        /// The error
        /// </summary>
        [Description("Error")]
        Error = 202,

        /// <summary>
        /// The API access invalid
        /// </summary>
        //[Description("Api access token invalid")]
        //ApiAccessInvalid = 203,
        [Description("Invalid credentials")]
        UserNotFound = 203,

        /// <summary>
        /// The authentication code invalid
        /// </summary>
        [Description("Authentication failed")]
        AuthCodeInvalid = 204,

        /// <summary>
        /// The device not sent
        /// </summary>
        [Description("Device not sent")]
        DeviceNotSent = 205,

        /// <summary>
        /// The offset not sent
        /// </summary>
        //[Description("Offset not sent")]
        //OffsetNotSent = 206,
        [Description("User deactivated")]
        UserDeactivated = 206,

        /// <summary>
        /// The user identifier not provided
        /// </summary>
        [Description("User id not provided")]
        UserIdNotProvided = 207,

        /// <summary>
        /// The user type not provided
        /// </summary>
        [Description("User type not provided")]
        UserTypeNotProvided = 208,

        /// <summary>
        /// The account not verified
        /// </summary>
        [Description("Account is not verified")]
        AccountNotVerified = 209,

        /// <summary>
        /// The account not created
        /// </summary>
        [Description("Account could not be created")]
        AccountNotCreated = 210,

        /// <summary>
        /// The user not found
        /// </summary>
        //[Description("User not found")]
        //UserNotFound = 211,

        /// <summary>
        /// The user not available
        /// </summary>
        [Description("User not available")]
        UserNotAvailable = 212,

        /// <summary>
        /// The user already exists
        /// </summary>
        [Description("User with requested information already exists")]
        UserAlreadyExists = 215,

        /// <summary>
        /// The not found
        /// </summary>
        NotFound = 404, //08-may-2018

        /// <summary>
        /// The notification reply success
        /// </summary>
        NotificationReplySuccess,

        /// <summary>
        /// The email required fb login
        /// </summary>
        [Description("Please provide email.")]
        EmailRequiredFBLogin = 221,

        /// <summary>
        /// The incorrect user login
        /// </summary>
        [Description("This login is related to Venue not a User")]
        IncorrectUserLogin = 222,

        /// <summary>
        /// The user login successfully
        /// </summary>
        [Description("User seccessfully logged In")]
        UserLoginSuccessfully = 223,

        /// <summary>
        /// The password is wrong
        /// </summary>
        [Description("Invalid credentials")]
        PasswordIsWrong = 224,

        /// <summary>
        /// NoRecords
        /// </summary>
        [Description("No Records found ")]
        NoRecords = 404,

        [Description("Bad Request")]
        BadRequest = 400,
    }
}
