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
                View.Children.Clear();
                View.Children.Add(errorMessage);
                View.Children.Add(reload);
            }
        }

        private async void Reload_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }

        void RenderPage()
        {
            ThreadList = Threads.ThreadList;
            BindingContext = this;
           /* View.Children.Clear();
            View2.Children.Clear();
            View3.Children.Clear();
            SearchBar search = new SearchBar();
            search.TextChanged += Search_TextChanged;
            search.SearchButtonPressed += Search_SearchButtonPressed;
            View.Children.Add(search);
            Button MakeNewThread = new Button();
            MakeNewThread.Text = Strings.MakeNewThread;
            MakeNewThread.Clicked += MakeNewThread_Clicked;
            View3.Children.Add(MakeNewThread);
            Button SavedThreads = new Button();
            SavedThreads.Text = Strings.SavedThreads;
            SavedThreads.Clicked += SavedThreads_Clicked;
            View.Children.Add(SavedThreads);
            foreach (Thread thread in Threads.ThreadList)
            {
                ThreadUI threadUI = new ThreadUI(thread);
                View2.Children.Add(threadUI.ThreadFrame());
            }*/

        }

        private void Search_SearchButtonPressed(object sender, EventArgs e)
        {
            //View2.Children.Clear();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            //SearchBar search = (SearchBar)sender;
            //View2.Children.Clear();
            //foreach(Thread thread in Threads.SearchThreads(search.Text)){
            //    ThreadUI threadUI = new ThreadUI(thread);
            //   View2.Children.Add(threadUI.ThreadFrame());
            //}
        }

        private void SavedThreads_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedThreadsPage());
        }

        private void MakeNewThread_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MakeThreadPage());
            //DisplayAlert("Alert", ThreadList.Count().ToString(), "OK");
        }

        private void ThreadListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Thread thread = e.Item as Thread;
            Navigation.PushAsync(new ThreadPage(thread.ID));
        }

    }
}