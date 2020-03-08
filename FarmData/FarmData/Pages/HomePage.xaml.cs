using FarmData.Data;
using FarmData.Models;
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
    public partial class HomePage : TabbedPage
    {

        public HomePage()
        {
            /*
            Task<bool> GetThreads = Threads.UpdateThreads();
            Task<bool> GetSaves = Threads.UpdateSavedThreadList();
            //Task<bool> GetLog = LogBook.UpdateLogList();
            Task<bool> GetReport = Reports.UpdateReports();
            TaskManager.Add(GetThreads);
            TaskManager.Add(GetSaves);
            TaskManager.Add(GetReport);
            
 
            TaskManager.Run();
            */
            InitializeComponent();
            Profile.Clicked += Profile_Clicked;
            //setup();
            
        }
        /*async void setup()
        {
            ForumPage forum = new ForumPage();
            SavedThreadsPage saved = new SavedThreadsPage();
            Children.Add(forum);
            Children.Add(saved);
            await Threads.UpdateThreads(forum);
            await Threads.UpdateSavedThreadList(saved);
            forum.BindingContext = forum;
        }*/

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
    }
}