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

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            //check valid username
            if (!(await Authentication.UserNameAvailable(UserNameEntry.Text)))
            {
                await DisplayAlert(Strings.Error, Strings.UserNameTaken, Strings.Ok);
                return;
            }
            //check valid password
            if (!(await Authentication.EmailAvailable(EmailEntry.Text)))
            {
                await DisplayAlert(Strings.Error, Strings.EmailTaken, Strings.Ok);
                return;
            }
            //check passwords match
            if(PasswordEntry.Text != RePasswordEntry.Text)
            {
                await DisplayAlert(Strings.Error, Strings.PasswordMismatch, Strings.Ok);
                return;
            }
            //check password length
            if(PasswordEntry.Text.Length < Authentication.minPasswordLength || PasswordEntry.Text.Length > Authentication.maxPasswordLength)
            {
                await DisplayAlert(Strings.Error, Strings.PasswordLength, Strings.Ok);
                return;
            }
            if(await Authentication.Register(EmailEntry.Text, PasswordEntry.Text, UserNameEntry.Text))
            {
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert(Strings.Error, Strings.Error, Strings.Ok);
            }
        }
    }
}