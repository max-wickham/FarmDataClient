﻿using FarmData.Data;
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
    public partial class AddCropPage : ContentPage
    {
        Position position = new Position();
        public AddCropPage()
        {
            InitializeComponent();
            Map map = new Map();
            map.MapClicked += Map_MapClicked;
            View.Children.Add(map);
            CropPicker.ItemsSource = Profile.Crops;
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

        private async void Save_Clicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            Tuple<double, double> location = new Tuple<double, double>(position.Latitude, position.Longitude);
            FarmInfo farmInfo = new FarmInfo(NameEntry.Text,(CropPicker.SelectedItem as string), location, (SizePicker.SelectedItem as string));
            if (await Profile.SaveNewProfile(farmInfo)) { await Navigation.PushAsync(new HomePage()); }
            (sender as Button).IsEnabled = true;
        }
    }
}