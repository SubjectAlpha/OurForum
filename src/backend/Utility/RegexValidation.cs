namespace backend.Utility;

public static class RegexValidation
{
    private static readonly string EmailRegex =
        @"^(?=.{1,254}$)(?:(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*)|(?:""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*""))@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)$";
    public static readonly string PasswordRegex =
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$";

    public static bool ValidateEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, EmailRegex);
    }

    public static bool ValidatePassword(string password)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(password, PasswordRegex);
    }
}
