﻿using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace Triggered
{
    public partial class Reports : ContentPage
    {
        public Reports()
        {
           
            InitializeComponent();
            Handler handle = new Handler();
            ReportsList.ItemsSource = handle.getDateTime();
        }
    }
}
