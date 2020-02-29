using FarmData.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.UIModels
{
    class CommentUI
    {
        Comment comment;
        static Frame createLabelFrame(View item)
        {
            Frame frame = new Frame();
            frame.Style = (Style)Application.Current.Resources["ThreadItemFrame"];
            frame.Content = item;
            frame.Padding = 1;
            return frame;
        }

        public CommentUI(Comment comment)
        {
            this.comment = comment;
        }
        public Frame CommentFrame()
        {
            Frame frame = new Frame();
            Grid grid = new Grid();
            Label user = new Label();
            Label date = new Label();
            Label description = new Label();
            Image photo = new Image();

            //set frame style
            frame.Style = (Style)Application.Current.Resources["ThreadFrame"];
            //set element Styles
            user.Style = (Style)Application.Current.Resources["ThreadUser"];
            date.Style = (Style)Application.Current.Resources["ThreadDate"];
            description.Style = (Style)Application.Current.Resources["ThreadDescription"];
            //Set the photo source
            if (comment.PhotoSource != null)
            {
                photo.Source = comment.PhotoSource;
            }
            //set the grid definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Auto) });

            //set the text of the elements
            user.Text = comment.UserName;
            date.Text = comment.CreationDate.ToString();
            description.Text = comment.Description;

            //Add the ui elements to the grid
            Frame userFrame = createLabelFrame(user);
            grid.Children.Add(userFrame, 1, 1);
            Grid.SetColumnSpan(userFrame, 1);

            Frame creationFrame = createLabelFrame(date);
            grid.Children.Add(creationFrame, 2, 1);
            Grid.SetColumnSpan(creationFrame, 1);

            Frame descriptionFrame = createLabelFrame(description);
            grid.Children.Add(descriptionFrame, 1, 2);
            Grid.SetColumnSpan(descriptionFrame, 2);
            Grid.SetRowSpan(descriptionFrame, 1);

            grid.Children.Add(photo, 1, 3);
            Grid.SetColumnSpan(photo, 2);

            //add the grid to the frame
            frame.Content = grid;

            return frame;
        }
    }
}
