namespace Shared.Constants
{
    public class UserConstants
    {
        public const string adminMail = "sanjeet@gmail.com";
        public const string mailPassword = "fmbj bgjh gibb cxho";

        public const string mailBody = "<html>" +
                                         "<body>" +
                                         "<h1>Hello Sir!</h1>" +
                                          "<h2>Your password has been changed. If this was not done by you then follow the steps to secure your account.</h2>" +
                                          "<br>" +
                                          "<h3>Is this your mail id:</h3>" + "<h3>{UserEmail}</h3>" +
                                          "</body>" +
                                         "</html>";

        public const string CouponMessage = "Coupon is Invalid or it has already been redeemed.";

        public const string DataAddedMessage = "Data added successfully!";

        public const string NotFound = "Id not found!";
        public const string DataDeletedMessage = "Data Deleted successfully!";
        public const string DataUpdatedMessage = "Data updated successfully!";
        public const string ErrorMessage = "Something went wrong!";
        public const string PasswordChanged = "Password Changed successfully!";
        public const string LogoutMessage = "Logout successfully!";
        public const string UnhandledError = "An unhandled error occurred";
        public const string UnauthourizedMessage = "username or password incorrect!";

        public const string EmailNotFound = "Please enter correct Email. There is no account associated with this email.";

        public const string InvalidPassword = "Password does not match old password";
    }
}
