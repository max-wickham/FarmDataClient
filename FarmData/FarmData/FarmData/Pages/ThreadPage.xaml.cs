using FarmData.Data;
using FarmData.Models;
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
    public partial class ThreadPage : ContentPage
    {
        Thread thread;
        
        public ThreadPage(Thread _thread)
        {
            InitializeComponent();
            thread = _thread;
            SaveButton.Text = Threads.IsSaved(thread) ? Strings.Unsave : Strings.Save;
            SaveButton.Clicked += SaveButton_Clicked;

            if (Comments.UpdateComments(thread))
            {
                RenderPage(thread);
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

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            ToolbarItem button = sender as ToolbarItem;
            if (Threads.IsSaved(thread)) { Threads.RemoveSavedThread(thread); }
            else {Threads.SaveNewThread(thread); }
            SaveButton.Text = Threads.IsSaved(thread) ? Strings.Unsave : Strings.Save;

        }

        private void Reload_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        void RenderPage(Thread thread)
        {
            ThreadUI threadUI = new ThreadUI(thread);
            Frame ThreadFrame = threadUI.ThreadCommentFrame();
            View.Children.Add(ThreadFrame);
            foreach(Comment comment in Comments.CommentList)
            {
                CommentUI commentUI = new CommentUI(comment);
                Frame commentFrame = commentUI.CommentFrame();
                View.Children.Add(commentFrame);
            }

            EntryGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            EntryGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Auto) });



        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            Comment comment = new Comment("", entry.Text);
            if(Comments.SendComment(comment, thread))
            {
                //need to set binding for scroll view ////////todo
            }

        }

    }
}