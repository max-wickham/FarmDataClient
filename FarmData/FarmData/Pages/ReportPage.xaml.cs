using FarmData.Data;
using FarmData.Models;
using FarmData.ModelsUI;
using FarmData.Resources;
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
    public partial class ReportPage : ContentPage
    {
        public ObservableCollection<Report> ReportList { get; private set; }
        public ReportPage()
        {
            InitializeComponent();
            
            if (Reports.UpdateReports())
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

        void RenderPage()
        {
            ReportList = Reports.ReportList;
            //ReportListView.ItemsSource = ReportList;
            BindingContext = this;
            /*StackLayout view = View;
            view.Children.Clear();
            Label cropLabel = new Label();
            cropLabel.Text = Strings.Crops;
            cropLabel.Style = (Style)Application.Current.Resources["ReportHeaderLabel"];
            Label weatherLabel = new Label();
            weatherLabel.Text = Strings.Weather;
            weatherLabel.Style = (Style)Application.Current.Resources["ReportHeaderLabel"];

            view.Children.Add(ReportUI.createLabelFrame(cropLabel));
            foreach (Crop cropReport in Reports.cropReportsList)
            {
             //   CropReportUI cropReportUI = new CropReportUI(cropReport);
             //   view.Children.Add(cropReportUI.Render());
            }

            view.Children.Add(ReportUI.createLabelFrame(weatherLabel));
            foreach (Weather weatherReport in Reports.weatherReportsList)
            {
             //   WeatherReportUI weatherReportUi = new WeatherReportUI(weatherReport);
             //   view.Children.Add(weatherReportUi.Render());
            }
        */
        }

        private void ReportListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //(e.SelectedItem as Report).Type
            DisplayAlert("Alert", (e.SelectedItem).GetType().ToString(), "OK"); 
        }
    }

    public class ReportDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CropTemplate { get; set; }
        public DataTemplate WeatherTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            return item.GetType() == typeof(Crop) ? CropTemplate : WeatherTemplate;
        }
    }
}