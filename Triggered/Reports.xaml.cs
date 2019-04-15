using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Triggered
{
    public partial class Reports : ContentPage
    {
        public Reports(List<string> DateTime)
        {
           
            InitializeComponent();
            ReportsList.ItemsSource = DateTime;
        }
    }
}
