using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FarmData.Models;
using FarmData.Pages;

namespace FarmData
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LogIn();
        }

        async void LogIn()
        {
            bool loggedin = false;
            loggedin = await Authentication.LogIn(Authentication.GetEmail(), Authentication.GetPassword());
            if (!loggedin)
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await Navigation.PushAsync(new HomePage());
            }
        }
    }
}
