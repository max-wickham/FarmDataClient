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
    public partial class AddFarmInfoPage : ContentPage
    {
        public AddFarmInfoPage()
        {
            InitializeComponent();
        }

        private void AddLiveStock_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddLiveStockPage());
        }

        private void AddCrop_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddCropPage());
        }
    }
}