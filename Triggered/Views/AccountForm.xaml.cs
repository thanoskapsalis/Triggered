using System;
using Acr.UserDialogs;
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
            try
            {
                UserDialogs.Instance.ShowLoading ();
                var user=new UserHandler(username.Text, password.Text);
                await user.Login ();
                int star=user.get_user_stars ();
                UserDialogs.Instance.HideLoading ();
                if (user.get_user_status ())
                    await Navigation.PushAsync(new MainPage(username.Text, star), true);

            }
            catch (Exception exception)
            {
                UserDialogs.Instance.Alert(exception.ToString ());
            }
        }

        private void Regtister_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register (), true);
        }
    }
}