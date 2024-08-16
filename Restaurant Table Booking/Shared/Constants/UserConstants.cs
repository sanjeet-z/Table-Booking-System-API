namespace Shared.Constants
{
    public class UserConstants
    {
        public const string adminMail = "peeyush.s2g@gmail.com";
        public const string mailPassword = "fmbj bgjh gibb cxho";

        public const string mailBody = "<html>" +
                                         "<body>" +
                                         "<h1>Hello Sir!</h1>" +
                                          "<h2>Your password has been changed. if You are not then recover your password and claim now!</h2>" +
                                          "<br>" +
                                          "<h3>Is this your mai id:</h3>" + "<h3>{UserEmail}</h3>" +
                                          "</body>" +
                                         "</html>";

        public const string CouponMessage = "Coupon is InValid! or it is already used!";

        public const string DataAddedMessage = "Data added successfully!";

        public const string NotFound = "Id not found!";
        public const string DataDeletedMessage = "Data Deleted successfully!";
        public const string DataUpdatedMessage = "Data updated successfully!";
        public const string ErrorMessage = "Something went wrong!";
        public const string PasswordChanged = "Password Change successfully!";
        public const string LogoutMessage = "Logout successfully!";
        public const string UnhandledError = "An unhandled error occurred";
        public const string UnauthourizedMessage = "Please enter correct username or password";

        public const string EmailNotFound = "Please write correct email this email does not have account.";

        public const string InvalidPassword = "Password does not match old password";
    }
}
