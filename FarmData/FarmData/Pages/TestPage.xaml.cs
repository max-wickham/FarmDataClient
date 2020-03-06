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
            string photoSource = null;
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                photoSource = photo.Path;
            }

            byte[] fileBytes;
        }

    }
}