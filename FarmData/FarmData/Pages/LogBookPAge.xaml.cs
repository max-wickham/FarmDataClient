using FarmData.Data;
using FarmData.Resources;
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
    public partial class LogBookPage : ContentPage
    {
        public ObservableCollection<Log> LogList { get; private set; }
        public LogBookPage()
        {
            InitializeComponent();
            setup();
        }
        async void setup()
        {
            if (await LogBook.UpdateLogList())
            {
                RenderPage();
            }
            else
            {
                Button reload = new Button();
                reload.Text = Strings.Reload;
                reload.Style = (Style)Application.Current.Resources["PrimaryButtonStyle"];
                reload.Clicked += Reload_Clicked;
                Label errorMessage = new Label();
                errorMessage.Text = Strings.ErrorLoading;
                errorMessage.Style = (Style)Application.Current.Resources["PrimaryLabelStyle"];
                View.Children.Add(errorMessage);
                View.Children.Add(reload);
            }
        }
        private void Reload_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        public void RenderPage()
        {
            LogList = LogBook.LogList;
            BindingContext = this;
        }

        private void AddLog_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddLogPage());
        }
    }
}