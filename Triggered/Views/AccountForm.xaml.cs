using System;
using Triggered.Classes;
using Triggered.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triggered
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountForm : ContentPage
    {
        public AccountForm ()
        {
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void LogIn_OnClicked(object sender, EventArgs e)
        {
            var user=new UserHandler(username.Text, password.Text);
            await user.Login ();
            if (user.get_user_status ())
                await Navigation.PushAsync(new MainPage(username.Text), true);
        }

        private void Regtister_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register (), true);
        }
    }
}