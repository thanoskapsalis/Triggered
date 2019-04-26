using System;
using Acr.UserDialogs;
using Triggered.Classes;
using Xamarin.Forms;

namespace Triggered
{
    public partial class MainPage : ContentPage
    {
        private int user_stars;
        public MainPage(string usernameText,int user_stars)
        {
            InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            this.user_stars=user_stars;
            active_user.Text=usernameText;
            BackgroundImage="level10.png";
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            string comment=null;
            var pResult=await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType=InputType.Name,
                OkText="Save",
                Title="What exactly have hi do"
            });
            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text)) comment=pResult.Text;
            var trigger=new TriggerDB(DateTime.Now, comment, active_user.Text);
            var handle=new Handler(trigger);
            var  toadd= new  UserHandler(active_user.Text,user_stars+1);
            toadd.Add_Stars ();
            UserDialogs.Instance.HideLoading ();

        }

       


        public void Handle_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reports (), true);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new About (), true);
        }

        private void Acc_manage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AccountForm (), true);
        }
    }
}