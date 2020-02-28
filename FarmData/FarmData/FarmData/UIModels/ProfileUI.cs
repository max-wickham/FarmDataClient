using FarmData.Data;
using FarmData.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.UIModels
{
    class ProfileUI
    {
        FarmInfo Info;
        public ProfileUI(FarmInfo info)
        { 
            Info = info;
        }
        public Frame CreateProfileFrame()
        {
            //create the ui elements
            StackLayout stack = new StackLayout();
            Frame frame = new Frame();
            Label Name = new Label();
            Label Size = new Label();
            Button Remove = new Button();
            //set the ui element styles
            Name.Style = (Style)Application.Current.Resources["ProfileFrameName"];
            Size.Style = (Style)Application.Current.Resources["ProfileFrameSize"];
            Remove.Style = (Style)Application.Current.Resources["ProfileFrameRemove"];
            //set the ui element text
            Name.Text = Info.Name;
            Size.Text = Strings.Squarekm + ": " + Info.Size.ToString();
            Remove.Text = Strings.Remove;
            //add the ui elements to the stack layout
            stack.Children.Add(Name);
            stack.Children.Add(Size);
            stack.Children.Add(Remove);
            //add the stacklayout to the frame
            frame.Content = stack;
            //return the frame
            return frame;
        }
    }
}
