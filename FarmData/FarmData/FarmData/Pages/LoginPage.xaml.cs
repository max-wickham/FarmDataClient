using FarmData.Models;
using FarmData.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FarmData.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LogInButton_Clicked(object sender, EventArgs e)
        {
            bool loggedin = await Authentication.LogIn(EmailEntry.Text, PasswordEntry.Text);//request a session key 
            if (!loggedin)
            {
                await DisplayAlert(Strings.Error, Strings.LoginAlert, Strings.Ok);//if returned session key is 0, unable to login, display warning
            }
            else
            {
                await Navigation.PushAsync(new Pages.HomePage());//load tabbed page
            }
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Pages.RegisterPage());//load registration page
        }
    }
}