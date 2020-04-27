using RPCSDK;
using System;


namespace UserSDK
{
    class User
    {
        public string name { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string username { get; set; }

        public User()
        {
        }

        public User(string[]user)
        {
            this.name = user[0];
            this.prenom = user[1];
            this.email = user[2];
            this.username = user[3];
        }

        public static User GetUser(string username)
        {
            var rpcClient = new RpcClient();

            Console.WriteLine(" [x] Requesting user " + username);
            var response = rpcClient.Call(username, "user_queue");

            Console.WriteLine(" [.] Got '{0}'", response);
            rpcClient.Close();

            //si l'username n'hexiste pas
            if (response == "null")
                return null;

            return new User(response.Split(":"));

        }
    }


    

}
