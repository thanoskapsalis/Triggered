namespace Triggered.Classes
{
    internal class UserData
    {
        public UserData(string username, string password , int stars)
        {
            this.username=username;
            this.password=password;
            this.stars=stars;
        }

        public UserData() { }

        public UserData(string username, int stars)
        {
            this.username=username;
            this.stars=stars;
        }

        

        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int stars { get; set; }
    }
}