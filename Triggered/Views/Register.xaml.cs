using System;
using Acr.UserDialogs;
using Triggered.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triggered.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        public Register ()
        {
            InitializeComponent ();
           
        }

        private async void Register_OnClicked(object sender, EventArgs e)
        {
            if (password.Text.Equals(password2.Text))
            {
                var handler=new UserHandler(username.Text, password.Text);
               await handler.Register ();
               if(handler.get_user_status ())
                UserDialogs.Instance.Alert("Head back to the Home Page and login with your new account","Account Created");
              

            }
            else
            {
                error.Text="Password mismatch";
            }
        }
    }
}