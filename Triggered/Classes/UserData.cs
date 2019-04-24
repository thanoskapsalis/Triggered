namespace Triggered.Classes
{
    internal class UserData
    {
        public UserData(string username, string password)
        {
            this.username=username;
            this.password=password;
        }

        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string stars { get; set; }
    }
}