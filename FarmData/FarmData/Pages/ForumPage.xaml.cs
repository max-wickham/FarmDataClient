using FarmData.Data;
using FarmData.Resources;
using FarmData.UIModels;
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
    public partial class ForumPage : ContentPage
    {
        //public List<String> TestList { get; set; }
        //public List<Frame> FrameList { get; set; }
        public ForumPage()
        {
            InitializeComponent();

            //FrameList = new List<Frame>();
            //TestList = new List<String>() { "1", "2", "3", "4", "5" };
            //listView.ItemTemplate = new DataTemplate(typeof(Cell));
            //listView.ItemsSource = FrameList;
            
            
            
            if (Threads.UpdateThreads())
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
            View.Children.Clear();
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
            }

        }

        private void Search_SearchButtonPressed(object sender, EventArgs e)
        {
            View2.Children.Clear();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar search = (SearchBar)sender;
            View2.Children.Clear();
            foreach(Thread thread in Threads.SearchThreads(search.Text)){
                ThreadUI threadUI = new ThreadUI(thread);
                View2.Children.Add(threadUI.ThreadFrame());
            }
        }

        private void SavedThreads_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SavedThreadsPage());
        }

        private void MakeNewThread_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MakeThreadPage());
        }
    
        public class Cell : ViewCell
        {
            //public static readonly BindableProperty FrameProperty =
            //    BindableProperty.Create("frame", typeof(Frame), typeof(Cell));
            
            public Cell()
            {
                var frameProperty = new Frame().Content;
                frameProperty.SetBinding(Frame.ContentProperty, "frame");
                var frame = new Frame();
                frame.Content = frameProperty;
                View = frame;
            }
        }
    
    }
}