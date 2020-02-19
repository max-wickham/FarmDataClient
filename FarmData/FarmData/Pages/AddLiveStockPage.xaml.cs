using FarmData.Data;
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
    public partial class AddLiveStockPage : ContentPage
    {
        Position position = new Position();
        public AddLiveStockPage()
        {
            InitializeComponent();
            Map map = new Map();
            map.MapClicked += Map_MapClicked;
            View.Children.Add(map);
            LiveStockPicker.ItemsSource = Profile.LiveStocks;
            SizePicker.ItemsSource = Profile.Sizes;
        }

        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            (sender as Map).Pins.Clear();
            Pin pin = new Pin();
            pin.Label = "Location";
            pin.Position = e.Position;
            (sender as Map).Pins.Add(pin);

            position = e.Position;
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            Tuple<double, double> location = new Tuple<double, double>(position.Latitude, position.Longitude);
            FarmInfo farmInfo = new FarmInfo((LiveStockPicker.SelectedItem as string), location, (SizePicker.SelectedItem as string));
            if (Profile.SaveNewProfile(farmInfo)) { Navigation.PushAsync(new HomePage()); }
        }
    }
}