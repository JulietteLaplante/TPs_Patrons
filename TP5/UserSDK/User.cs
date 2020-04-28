using RPCSDK;
using System;
using System.Text.Json;

namespace UserSDK
{
    public class User
    {
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public User()
        {
        }

        public User(string[] user)
        {
            this.lastname = user[0];
            this.firstname = user[1];
            this.email = user[2];
            this.username = user[3];
        }

        public static User GetUser(string username)
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" Requesting user " + username);
            var response = rpcClient.Call(username, "user_queue");

            rpcClient.Close();

            //si l'username n'hexiste pas
            if (response == "")
                return null;
            Console.WriteLine("User received.");

            try
            {
                return JsonSerializer.Deserialize<User>(response);
            } catch (Exception e)
            {
                return null;
            }

        }
    }




}
