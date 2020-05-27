using FarmData.Data;
using FarmData.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
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
             if(photoSource != null && photoSource != "")
             {
                byte[] imageBinary = File.ReadAllBytes(photoSource);
                string image = Convert.ToBase64String(imageBinary, 0, imageBinary.Length);//
                data["image"] = image;
                data["imagetype"] = System.IO.Path.GetExtension(photoSource);
             }
             Thread thread = new Thread(TitleEntry.Text,Authentication.Email, photoSource, DescriptionEntry.Text,"",0,0);
             if(await Threads.PostNewThread(data))
             {
                 await Navigation.PushAsync(new HomePage());
             }
             SaveButton.IsEnabled = true;
            
            //Dictionary<string, string> data = new Dictionary<string, string>();
            //data["title"] = TitleEntry.Text;
            //data["description"] = DescriptionEntry.Text;

            //byte[] imageBinary = File.ReadAllBytes(photoSource);
            //string image = Convert.ToBase64String(imageBinary, 0, imageBinary.Length);//
            //data["image"] = image;

            //string response = await new Request(new HTTPHandler()).Post("/getcreatethread",null,"max","12345",photoSource);
            //await DisplayAlert("Alert", response, "OK");
        }

        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    var photo = await CrossMedia.Current.PickPhotoAsync();
                    photoSource = photo.Path;
                }
            }
            catch { }
        }
    }
}