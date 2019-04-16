using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Triggered
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void Handle_Clicked(Object sender, EventArgs e)
        {
            String comment = null;
            PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Name,
                OkText = "Save",
                Title = "What exactly have hi do",
            });
            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                comment = pResult.Text.ToString();
            }
            TriggerDB trigger = new TriggerDB(DateTime.Now, comment);
            Handler handle = new Handler(trigger);
        }



         public void Handle_Clicked_1(object sender, System.EventArgs e)
        {

            this.Navigation.PushAsync(new Reports(), true);




        }

         private void Button_OnClicked(object sender, EventArgs e)
         {
             this.Navigation.PushAsync(new About(), true);
         }
    }
}
