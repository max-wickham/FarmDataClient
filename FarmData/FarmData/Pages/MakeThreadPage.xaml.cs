using FarmData.Data;
using FarmData.Models;
using Plugin.Media;
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
    public partial class MakeThreadPage : ContentPage
    {
        private string photoSource = null;
        public MakeThreadPage()
        {
            InitializeComponent();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["title"] = TitleEntry.Text;
            data["description"] = DescriptionEntry.Text;
            Thread thread = new Thread(TitleEntry.Text,Authentication.Email, photoSource, DescriptionEntry.Text,DateTime.Now,0,0);
            if(await Threads.PostNewThread(TitleEntry.Text, DescriptionEntry.Text))
            {
                await Navigation.PushAsync(new HomePage());
            }
            SaveButton.IsEnabled = true;
        }

        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                photoSource = photo.Path;
            }
        }
    }
}