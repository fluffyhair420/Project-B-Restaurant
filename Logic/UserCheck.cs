using System;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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
                Console.WriteLine("Invalid email format.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Email address cannot be empty.");
            }

            if (isValidEmail)
            {
                return true;
            }

            else
            {
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
    
        public static bool UsernameCheck(string username)
        {
            string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/User.json"));
            string json = File.ReadAllText(path);
            dynamic data = JsonConvert.DeserializeObject(json);
            bool userExist = false;
            if (data != null)
            {
                foreach (var item in data)
                {
                    // UserName already exists
                    if (item.UserName == username)
                    {
                        Console.WriteLine("This username is already in use. Please pick another username.");
                        userExist = true;
                        return false;
                    }
                }
            }
            // username doesn't yet exist and is valid
            if (!userExist)
            {
                return true;
            }

            // json file is empty so username can't exist > is valid
            else
            {
                return true;
            }
            
        }

        public static bool IsUsernameValid(string username)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]+$");
            if (regex.IsMatch(username))
            {
                return UserCheck.UsernameCheck(username);
            }
            else
            {
                Console.WriteLine("Please only use letters and numbers in your username.");
                return false;
            }
        }

        public static bool IsAlphabetic(string input)
        {
            Regex regex = new Regex(@"^[a-zA-Z\s]+$");
            return regex.IsMatch(input);
        }

        public static bool isNameAlphabetic(string input)
        {
            Regex regex = new Regex(@"^[a-zA-Z-\s]+$");
            return regex.IsMatch(input);
        }

        public static bool isCityAlphabetic(string input)
        {
            Regex regex = new Regex(@"^[a-zA-Z/\s]+$");
            return regex.IsMatch(input);
        }

        public static bool IsNumeric(string input)
        {
            Regex regex = new Regex(@"^[0-9\s]+$");
            return regex.IsMatch(input);
        }

        public static bool IsZipCodeValid(string input)
        {
            Regex regex = new Regex(@"^\d{4}[a-zA-Z]{2}$");
            return regex.IsMatch(input);
        }

        public static string GetValidInput(string prompt, Func<string, bool> validationFunc, string errorMessage)
        {
            string userInput = "";
            bool isValidInput = false;
            while (!isValidInput)
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();
                if (validationFunc(userInput))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
            return userInput;
        }
    }
}