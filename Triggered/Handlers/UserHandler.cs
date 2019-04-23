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
        private bool greenlight;
        private bool user_status;

        private readonly IMobileServiceTable<UserData> UserData=App.client.GetTable<UserData> ();

        public UserHandler(string username, string password)
        {
            this.username=username;
            this.password=password;
        }


        public async Task Register ()
        {
            await CheckFree(UserData);
            if (greenlight)
            {
                byte[] salt;
                new RNGCryptoServiceProvider ().GetBytes(salt=new byte[16]);
                var pbkdf2=new Rfc2898DeriveBytes(password, salt, 10000);
                var hash=pbkdf2.GetBytes(20);
                var hasBytes=new byte[36];
                Array.Copy(salt, 0, hasBytes, 0, 16);
                Array.Copy(hash, 0, hasBytes, 16, 20);
                var savedPasswordHash=Convert.ToBase64String(hasBytes);
                var user=new UserData(username, savedPasswordHash);
                await UserData.InsertAsync(user);
                user_status=true;
                UserDialogs.Instance.HideLoading ();
            }
            else
            {
                user_status=false;
            }
        }

        public async Task Login ()
        {
            //UserDialogs.Instance.Loading("Sign in");
            var result=await UserData.Where(x => x.username.Contains(username)).ToListAsync ();
            if (result.Count == 1)
                foreach (var i in result)
                {
                    var savedPasswordHash=i.password;
                    var hashBytes=Convert.FromBase64String(savedPasswordHash);
                    var salt=new byte[16];
                    Array.Copy(hashBytes, 0, salt, 0, 16);
                    var pbkfd2=new Rfc2898DeriveBytes(password, salt, 10000);
                    var hash=pbkfd2.GetBytes(20);
                   // UserDialogs.Instance.HideLoading ();
                    if (GrantAccess(hashBytes, hash))
                    {
                        user_status=true;
                    }
                    else
                    {
                       // UserDialogs.Instance.HideLoading ();
                        await UserDialogs.Instance.AlertAsync("Wrong Username or Password");
                    }
                }
        }


        public async Task CheckFree(IMobileServiceTable<UserData> userList)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Registering");
                var result=await userList.Where(x => x.username.Contains(username)).ToListAsync ();
                if (result.Count != 0)
                {
                    UserDialogs.Instance.HideLoading ();
                    UserDialogs.Instance.Alert("Account could not be created.Do you already have an account?Please try again later or contact the Developer.","Oh No!");
                }

                else
                {
                    greenlight=true;
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert(e.ToString ());
            }
        }

        public bool GrantAccess(byte[] hashBytes, byte[] hash)
        {
            var toreturn=true;
            for (var i=0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    toreturn=false;
            return toreturn;
        }

        public bool get_user_status ()
        {
            return user_status;
        }
    }
}