using System;
using System.Collections.Generic;
using System.Text;

namespace Triggered.Classes
{
    class UserData
    {
        public  string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public UserData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        
    }
}
