using Newtonsoft.Json;
namespace Restaurant
{
    public static class CurrentUserJson
    {
        private static string currentUserPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/CurrentUser.json"));
        private static string currentUserJson = File.ReadAllText(currentUserPath);
        static dynamic currentUserData = JsonConvert.DeserializeObject(currentUserJson);

        public static void CurrentUserLogout()
        {
            currentUserJson = string.Empty;
            File.WriteAllText(currentUserPath, currentUserJson);
        }

        public static void WriteCurrentUserToJson(dynamic item)
        {
            Address currentUserAddress = new Address
            {
                City = item.Address[0].City,
                Street = item.Address[0].Street,
                HouseNumber = item.Address[0].HouseNumber,
                ZipCode = item.Address[0].ZipCode
            };

            CurrentUserInfo currentUser = new CurrentUserInfo
            {
                UserID = item.UserID,
                UserName = item.UserName, 
                PassWord = item.PassWord,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                Address = new List<Address> { currentUserAddress }
            };

            // write current user to CurrentUser json
            string currentUserJson = JsonConvert.SerializeObject(currentUser, Formatting.Indented);
            File.WriteAllText(currentUserPath, currentUserJson);
        }
    }
}