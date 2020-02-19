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

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            Thread thread = new Thread(TitleEntry.Text,Authentication.Email, photoSource, DescriptionEntry.Text,DateTime.Now,0);
            Threads.SaveNewThread(thread);
            Navigation.PushAsync(new HomePage());
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