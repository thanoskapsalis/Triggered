using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Triggered
{
    public partial class Reports : ContentPage
    {
        public  Reports()
        {
          
            InitializeComponent();
            Run();
            
        }

        //Wait for server to load and then print the results
        public async void Run()
        {
            Handler handle = new Handler();
           await handle.LoadStuff();
           ReportsList.ItemsSource = handle.getDateTime();
        }
    }
}
