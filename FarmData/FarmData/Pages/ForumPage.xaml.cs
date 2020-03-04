using FarmData.Data;
using FarmData.Models;
using FarmData.Resources;
using FarmData.UIModels;
using Newtonsoft.Json;
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
    public partial class ForumPage : ContentPage
    {
        //public IList<Thread> ThreadList { get; private set; }
        public ObservableCollection<Thread> ThreadList { get; private set; }
        public ForumPage()
        {
            InitializeComponent();
            setup();
       
        }
        async void setup()
        {
            if (await Threads.UpdateThreads())
            {
                ThreadList = Threads.ThreadList;
                BindingContext = this;
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
                View.Children.Clear();
                View.Children.Add(errorMessage);
                View.Children.Add(reload);
            }
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        private void Search_SearchButtonPressed(object sender, EventArgs e)
        {
            //View2.Children.Clear();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SavedThreads_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedThreadsPage());
        }

        private async void MakeNewThread_Clicked(object sender, EventArgs e)
        {
            string response = await Request.Get("/getsaves", Authentication.Email, Authentication.Password);
            await DisplayAlert("Alert", response, "OK");
            //Navigation.PushAsync(new MakeThreadPage());
        }

        private void ThreadListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Thread thread = e.Item as Thread;
            Navigation.PushAsync(new ThreadPage(thread.ID));
        }

    }
}