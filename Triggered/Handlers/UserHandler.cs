using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.WindowsAzure.MobileServices;

namespace Triggered.Classes
{
    internal class UserHandler
    {
        private readonly string password;
        private readonly string username;
        private bool user_status;
        private bool greenlight;

        IMobileServiceTable<UserData> UserData = App.client.GetTable<UserData>();

        public UserHandler(string username, string password)
        {
            this.username = username;
            this.password = password;
           
        }


        public async void Register()
        {
            
            await CheckFree(UserData);
            if (greenlight)
            {
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                var hash = pbkdf2.GetBytes(20);
                var hasBytes = new byte[36];
                Array.Copy(salt, 0, hasBytes, 0, 16);
                Array.Copy(hash, 0, hasBytes, 16, 20);
                var savedPasswordHash = Convert.ToBase64String(hasBytes);
                var user = new UserData(username, savedPasswordHash);
                await UserData.InsertAsync(user);
                UserDialogs.Instance.HideLoading();
            }
        
        }

        public async Task Login()
        {
            UserDialogs.Instance.Loading("Sign in");
            var result = await UserData.Where(x => x.username.Contains(username)).ToListAsync();
            if (result.Count == 1)
            {
                foreach (var i in result)
                {
                    string savedPasswordHash = i.password.ToString();
                    byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                    byte[] salt = new byte[16];
                    Array.Copy(hashBytes,0,salt,0,16);
                    var pbkfd2 = new Rfc2898DeriveBytes(password,salt,10000);
                    byte[] hash = pbkfd2.GetBytes(20);
                    if (GrantAccess(hashBytes,hash))
                    {
                        UserDialogs.Instance.HideLoading();
                        user_status = true;
                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        await UserDialogs.Instance.AlertAsync("Wrong Username or Password");
                    }

                }
            }


        }


        public async Task CheckFree(IMobileServiceTable<UserData> userList)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Registering");
                var result = await userList.Where(x => x.username.Contains(username)).ToListAsync();
                if (result.Count != 0)
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert("Username Already exists");
                }

                else
                {
                    greenlight = true;
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.ToString());
            }
        }

        public bool GrantAccess(byte[] hashBytes,byte[] hash)
        {
            bool toreturn = true;
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    toreturn =false;
                
            }
            return toreturn;

        }

        public bool get_user_status() => user_status;
    }
}