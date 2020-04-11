using BaseUserLoaderPlugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace JSONUserLoaderPlugin
{
    public class JSONUserLoader : IUserLoader
    {
        public string Name { get => "json-loader"; }
        public string Description { get => "Loads and displays users defined in json in the 'users' directory."; }


        public List<User> LoadUsers()
        {
            List<User> result = new List<User>();

            // Find files
            string[] files = Directory.GetFiles("users", "*.json");

            // Create users
            foreach (string file in files)
            {
                Console.WriteLine($"Reading user file '{file}'");
                try
                {
                    result.Add(
                        JsonSerializer.Deserialize<User>(
                            File.ReadAllText(file)
                            )
                        );
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Warning: Failed to read user from '{file}' : {e.Message}");
                }
            }

            return result;
        }
    }
}