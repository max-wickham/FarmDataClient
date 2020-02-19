using FarmData.Data;
using FarmData.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FarmData.Pages;

namespace FarmData.UIModels
{
    class ThreadUI
    {
        bool saved = false;

        Thread thread;
        static Frame createLabelFrame(View item)
        {
            Frame frame = new Frame();
            frame.Style = (Style)Application.Current.Resources["ThreadItemFrame"];
            frame.Content = item;
            frame.Padding = 1;
            return frame;
        }
        public ThreadUI(Thread thread)
        {
            this.thread = thread;
        }

        public Frame ThreadFrame()
        {
            
            //create ui elements
            Frame frame = new Frame();
            Grid grid = new Grid();
            Label title = new Label();
            Label user = new Label();
            Label date = new Label();
            Label commentCount = new Label();
            Label description = new Label();
            Image photo = new Image();
            Button save = new Button();
            Button view = new Button();

            //set frame style
            frame.Style = (Style)Application.Current.Resources["ThreadFrame"];

            //set element Styles
            title.Style = (Style)Application.Current.Resources["ThreadTitle"];
            user.Style = (Style)Application.Current.Resources["ThreadUser"];
            date.Style = (Style)Application.Current.Resources["ThreadDate"];
            description.Style = (Style)Application.Current.Resources["ThreadDescription"];
            commentCount.Style = (Style)Application.Current.Resources["ThreadCommentCount"];
            save.Style = (Style)Application.Current.Resources["ThreadSave"];
            view.Style = (Style)Application.Current.Resources["ThreadView"];

            //Set the photo source
            if(thread.PhotoSource != null){
                photo.Source = thread.PhotoSource;
            }
            //set the grid definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Auto) });

            //set the text of the elements
            title.Text = thread.Title;
            user.Text = thread.UserName;
            date.Text = thread.CreationDate.ToString();
            commentCount.Text = thread.CommentCount.ToString();
            description.Text = thread.Description;
            //TODO check if thread is saved and change save text accordingly
            //TODO add functionality to save and view
            save.Text = Strings.Save;
            foreach (Thread savedthread in Threads.SavedThreads)
            {
                if(savedthread.ID == thread.ID){
                    save.Text = Strings.Unsave;                  
                    saved = true;
                }
            }
            save.Text = Strings.Save;
            view.Text = Strings.View;

            //Add the button events
            view.Clicked += View_Clicked;
            save.Clicked += Save_Clicked;

            //Add the ui elements to the grid
            Frame userFrame = createLabelFrame(user);        
            grid.Children.Add(userFrame, 1, 1);
            Grid.SetColumnSpan(userFrame, 1);

            Frame titleFrame = createLabelFrame(title);
            grid.Children.Add(titleFrame, 2, 1);
            Grid.SetColumnSpan(titleFrame, 3);

            Frame descriptionFrame = createLabelFrame(description);
            grid.Children.Add(descriptionFrame, 1, 2);
            Grid.SetColumnSpan(descriptionFrame, 4);
            Grid.SetRowSpan(descriptionFrame, 2);

            Frame commentCountFrame = createLabelFrame(commentCount);
            grid.Children.Add(commentCountFrame, 1,4);
            Grid.SetColumnSpan(commentCountFrame, 2);

            Frame saveFrame = createLabelFrame(save);
            grid.Children.Add(saveFrame, 3, 4);
            Grid.SetColumnSpan(saveFrame, 1);

            Frame viewFrame = createLabelFrame(view);
            grid.Children.Add(viewFrame, 4, 4);
            Grid.SetColumnSpan(viewFrame, 1);

            grid.Children.Add(photo,1,5);
            Grid.SetColumnSpan(photo,4);

            //add the grid to the frame
            frame.Content = grid;


            //return the frame
            return frame;
        }

        public Frame ThreadCommentFrame()
        {
            //create ui elements
            Frame frame = new Frame();
            Grid grid = new Grid();
            Label title = new Label();
            Label user = new Label();
            Label date = new Label();
            Label description = new Label();
            Image photo = new Image();


            //set frame style
            frame.Style = (Style)Application.Current.Resources["ThreadFrame"];

            //set element Styles
            title.Style = (Style)Application.Current.Resources["ThreadTitle"];
            user.Style = (Style)Application.Current.Resources["ThreadUser"];
            date.Style = (Style)Application.Current.Resources["ThreadDate"];
            description.Style = (Style)Application.Current.Resources["ThreadDescription"];

            //Set the photo source
            if (thread.PhotoSource != null)
            {
                photo.Source = thread.PhotoSource;
            }
            //set the grid definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(4, GridUnitType.Auto) });

            //set the text of the elements
            title.Text = thread.Title;
            user.Text = thread.UserName;
            date.Text = thread.CreationDate.ToString();
            description.Text = thread.Description;

            //Add the ui elements to the grid
            Frame userFrame = createLabelFrame(user);
            grid.Children.Add(userFrame, 1, 1);
            Grid.SetColumnSpan(userFrame, 1);

            Frame titleFrame = createLabelFrame(title);
            grid.Children.Add(titleFrame, 2, 1);
            Grid.SetColumnSpan(titleFrame, 3);

            Frame descriptionFrame = createLabelFrame(description);
            grid.Children.Add(descriptionFrame, 1, 2);
            Grid.SetColumnSpan(descriptionFrame, 4);
            Grid.SetRowSpan(descriptionFrame, 2);

            grid.Children.Add(photo, 1, 4);
            Grid.SetColumnSpan(photo, 4);

            //add the grid to the frame
            frame.Content = grid;

            //return the frame
            return frame;
        }
        private void Save_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if(saved){
                if(Threads.RemoveSavedThread(thread)){
                    saved = false;
                    button.Text = Strings.Save;
                }
            }else{
            if(Threads.SaveNewThread(thread)){
                    saved = true;
                    button.Text = Strings.Unsave;
                }
            }
        }

        private void View_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new ThreadPage(thread));
        }
    }
}
