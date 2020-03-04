using FarmData.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FarmData.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        async void Photopick_Clicked(object sender, EventArgs e)
        {
            var dic = new Dictionary<string, string>()
            {
                { "username","max" }
            };
            string response = await Request.Post(entry.Text,dic);

            await DisplayAlert("Alert", response, "OK");
        }

    }
}