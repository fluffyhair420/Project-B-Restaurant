using Newtonsoft.Json;

namespace Restaurant
{
    class UpdateJson
    {

        public UpdateJson()
        {

        }
        public void writeToJson(string firstName, string lastName, string userName, string phoneNumber,
        string passWord, string email, List<Address> address, string path)
        {
            // check if json file is not empty
            if (File.Exists(path) && new FileInfo(path).Length > 0)
            {
                // read json content
                string json = File.ReadAllText(path);

                // Deserialize the JSON into a list of User objects
                List<UserInfo> users = JsonConvert.DeserializeObject<List<UserInfo>>(json);


                // Create a list of user information
                UserInfo newUser = new UserInfo
                {
                    UserID = users.Count > 0 ? users[users.Count - 1].UserID + 1 : 1,
                    UserName = userName,
                    PassWord = passWord,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address
                };
                users.Add(newUser);


                // Convert the list to JSON
                json = JsonConvert.SerializeObject(users, Formatting.Indented);

                // Write the JSON to a file
                File.WriteAllText(path, json);
            }

            // if json file is empty, create first account with ID number 1
            else
            {
                // Create a list of user information
                List<UserInfo> firstUser = new List<UserInfo>
                {
                    new UserInfo { UserID = 1,
                    UserName = userName, PassWord = passWord, FirstName = firstName, LastName = lastName,
                    Email = email, PhoneNumber = phoneNumber, Address = address}
                };

                // Convert the list to JSON
                string json = JsonConvert.SerializeObject(firstUser, Formatting.Indented);

                // Write the JSON to a file
                File.WriteAllText(path, json);
            }

        }

    }
}
