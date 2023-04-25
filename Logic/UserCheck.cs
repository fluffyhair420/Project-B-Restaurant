using System;
using System.Net.Mail;

namespace Restaurant
{
    public static class UserCheck
    {
        public static string UserEmail;
        public static string UserPassword;


        public static bool EmailCheck(string useremail)
        {
            UserEmail = useremail;
            bool isValidEmail = false;

            try
            {
                MailAddress mailAddress = new MailAddress(UserEmail);
                isValidEmail = true;
            }
            catch (FormatException)
            {
                // The user input is not a valid email format
            }

            if (isValidEmail)
            {
                return true;
            }

            else
            {
                Console.WriteLine("Invalid email address.");
                return false;
            }
        }

        public static bool PasswordCheck(string userpassword)
        {
            UserPassword = userpassword;
            bool containsNumber = false;
            bool containsCapital = false;

            if (UserPassword == null || UserPassword.Length < 8)
            {
                return false;
            }

            foreach (char character in UserPassword)
            {
                if (char.IsDigit(character))
                {
                    containsNumber = true;
                }

                if (char.IsUpper(character))
                {
                    containsCapital = true;
                }
            }

            if (containsNumber == true && containsCapital == true)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


    }
}