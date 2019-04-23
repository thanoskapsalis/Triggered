using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triggered.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Triggered
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountForm : ContentPage
    {

        public AccountForm()
        {
            InitializeComponent();

        }

        private async void LogIn_OnClicked(object sender, EventArgs e)
        {
           UserHandler user = new UserHandler(username.Text,password.Text);
           user.Register();
        }
    }
}