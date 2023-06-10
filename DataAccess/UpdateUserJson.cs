using Newtonsoft.Json;

namespace Restaurant
{
    class UpdateJson
    {
        public UpdateJson()
        {

        }
        public void writeToJson(UserInfo newUser, string path)
        {
            // check if json file is not empty
            if (File.Exists(path) && new FileInfo(path).Length > 0)
            {
                // read json content
                string json = File.ReadAllText(path);

                // Deserialize the JSON into a list of User objects
                List<UserInfo> users = JsonConvert.DeserializeObject<List<UserInfo>>(json);

                // Create UserID (add 1 compared to previous user in the list)
                int nextUserID = users.Count > 0 ? users[users.Count - 1].UserID + 1 : 1;
                newUser.UserID = nextUserID;

                // add user to the user list
                users.Add(newUser);

                // Convert the list to JSON
                json = JsonConvert.SerializeObject(users, Formatting.Indented);

                // Write the JSON to a file
                File.WriteAllText(path, json);
            }

            // if json file is empty, create first account with ID number 1
            else
            {
                newUser.UserID = 1;

                // Create a list of user information
                List<UserInfo> firstUser = new List<UserInfo>
                {
                    newUser
                };

                // Convert the list to JSON
                string json = JsonConvert.SerializeObject(firstUser, Formatting.Indented);

                // Write the JSON to a file
                File.WriteAllText(path, json);
            }
        }
    }
}
