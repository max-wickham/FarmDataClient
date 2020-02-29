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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            //check valid username
            if (!Authentication.UserNameAvailable(UserNameEntry.Text))
            {
                DisplayAlert(Strings.Error, Strings.UserNameTaken, Strings.Ok);
                return;
            }
            //check valid password
            if (!Authentication.EmailAvailable(EmailEntry.Text))
            {
                DisplayAlert(Strings.Error, Strings.EmailTaken, Strings.Ok);
                return;
            }
            //check passwords match
            if(PasswordEntry.Text != RePasswordEntry.Text)
            {
                DisplayAlert(Strings.Error, Strings.PasswordMismatch, Strings.Ok);
                return;
            }
            //check password length
            if(PasswordEntry.Text.Length < Authentication.minPasswordLength || PasswordEntry.Text.Length > Authentication.maxPasswordLength)
            {
                DisplayAlert(Strings.Error, Strings.PasswordLength, Strings.Ok);
                return;
            }
            Authentication.Register(EmailEntry.Text, PasswordEntry.Text, UserNameEntry.Text);
            Navigation.PushAsync(new LoginPage());
        }
    }
}