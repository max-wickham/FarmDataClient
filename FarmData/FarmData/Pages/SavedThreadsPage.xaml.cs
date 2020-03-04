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
    public partial class SavedThreadsPage : ContentPage
    {
        //public IList<Thread> ThreadList { get; private set; }
        public ObservableCollection<Thread> SavedThreadsList { get; private set; }
        public SavedThreadsPage()
        {
            InitializeComponent();
            setup();

        }
        async void setup()
        {
            if (await Threads.UpdateSavedThreadList())
            {
                SavedThreadsList = Threads.SavedThreads;
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


        private void ThreadListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Thread thread = e.Item as Thread;
            Navigation.PushAsync(new ThreadPage(thread.ID));
        }

    }
}