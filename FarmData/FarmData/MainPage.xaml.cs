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
            //TODO recall email
            //TODO recall password
            //TODO generate clientID
            SessionData.sessionKey = Authentication.LogIn(Authentication.GetEmail(), Authentication.GetPassword());
            if (SessionData.sessionKey == 0)
            {
                Navigation.PushAsync(new LoginPage());
            }
            else
            {
                Navigation.PushAsync(new Pages.HomePage());
            }
        }
    }
}
