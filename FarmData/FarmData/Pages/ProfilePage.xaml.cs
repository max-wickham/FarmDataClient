using FarmData.Data;
using FarmData.Models;
using FarmData.UIModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<FarmInfo> FarmInfoList {get; set;}
        public ProfilePage()
        {
            InitializeComponent();
            RenderPage();
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

        public async void RenderPage()
        {
            if (await Profile.UpdateFarmProfile())
            {
                BindingContext = this;
                FarmInfoList = Profile.FarmProfile;
            }
            else
            {
                error();
            }

        }
        public void error()
        {
            //needs implementing
        }
    }
}