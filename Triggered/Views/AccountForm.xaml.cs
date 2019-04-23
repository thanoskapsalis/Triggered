using System;
using Triggered.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triggered
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountForm : ContentPage
    {
        private bool user_status;
        private string active_username;
        public AccountForm()
        {
            InitializeComponent();
        }

        private void LogIn_OnClicked(object sender, EventArgs e)
        {
            var user = new UserHandler(username.Text, password.Text);
            user.Login();
            if (user.get_user_status())
                this.Navigation.PushAsync(new MainPage(username.Text), true);

        }

        private void Regtister_OnClicked(object sender, EventArgs e)
        {
            var user = new UserHandler(username.Text, password.Text);
            user.Register();
        }
    }
}