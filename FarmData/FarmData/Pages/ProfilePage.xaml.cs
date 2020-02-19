using FarmData.Data;
using FarmData.Models;
using FarmData.UIModels;
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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            Profile.UpdateFarmProfile();
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            if (Authentication.LogOut())
            {
                Navigation.PushAsync(new LoginPage());
            }
        }

        private void AddProfile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddFarmInfoPage());
        }

        public void RenderPage()
        {
            foreach(FarmInfo info in Data.Profile.FarmProfile)
            {
                ProfileUI profileUI = new ProfileUI(info);
                Frame frame = profileUI.CreateProfileFrame();
                View.Children.Add(frame);
            }
        }
    }
}