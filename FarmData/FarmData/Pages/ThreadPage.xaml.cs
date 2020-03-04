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
    public partial class ThreadPage : ContentPage
    {
        int id;
        Thread thread;
        //public IList<Comment> CommentList { get; private set; }
        public ObservableCollection<Comment> CommentList { get; private set; } 
        public ThreadPage(int id)
        {
            InitializeComponent();
            this.id = id;
            setup();

        }
        async void setup()
        {
            string response = await Threads.GetThread(id);
            try
            {
                if (response != "error")
                {
                    Dictionary<string, string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                    thread = new Thread(values["title"], values["user"], null, values["description"], DateTime.Now, 0, id);

                    SaveButton.Text = Threads.IsSaved(thread) ? Strings.Unsave : Strings.Save;
                    SaveButton.Clicked += SaveButton_Clicked;

                    if (await Comments.UpdateComments(id))
                    {
                        RenderPage(thread);
                    }
                    else
                    {
                        error();
                    }
                }
                else { error(); }
            }
            catch
            {
                error();
            }
        }

        void error()
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

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;
            ToolbarItem button = sender as ToolbarItem;
            if (Threads.IsSaved(thread)) { await Threads.RemoveSavedThread(thread); }
            else {await Threads.SaveNewThread(thread); }
            SaveButton.Text = Threads.IsSaved(thread) ? Strings.Unsave : Strings.Save;
            SaveButton.IsEnabled = true;
        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        void RenderPage(Thread thread)
        {
            ThreadUserLabel.Text = thread.UserName;
            ThreadTitleLabel.Text = thread.Title;
            ThreadDescriptionLabel.Text = thread.Description;

            CommentList = Comments.CommentList;
            BindingContext = this;


            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            //    Device.BeginInvokeOnMainThread(() => Comments.UpdateComments(id));
            //    return true;
            //});
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            send.IsEnabled = false;
            Comment comment = new Comment(Authentication.Email, entry.Text);
            if(await Comments.SendComment(comment, id))
            {
                CommentList = Comments.CommentList;
            }
            send.IsEnabled = true;

        }

    }
}