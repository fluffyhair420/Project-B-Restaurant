using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Restaurant
{
    public static class ReadCurrentUserJson
    {
        static string filePath = "DataSources/CurrentUser.json";
        public static CurrentUserInfo GetCurrentUserInfo()
        {
            CurrentUserInfo currentUser = new CurrentUserInfo();

            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    string json = streamReader.ReadToEnd();

                    if (!string.IsNullOrEmpty(json))
                    {
                        currentUser = JsonConvert.DeserializeObject<CurrentUserInfo>(json);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"ERROR: File not found. Please check Reserve.GetCurrentUserInfo() method. {e.Message}.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: Something went wrong. Please check Reserve.GetCurrentUserInfo() method. {e.Message}");
            }
            return currentUser;
        }
    }
}