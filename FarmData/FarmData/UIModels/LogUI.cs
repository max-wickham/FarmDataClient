using FarmData.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.UIModels
{
    class LogUI
    {
        public static Frame createLabelFrame(View item)
        {
            Frame frame = new Frame();
            frame.Style = (Style)Application.Current.Resources["ReportFrame"];
            frame.Content = item;
            return frame;
        }

        Log log;
        public LogUI(Log log)
        {
            this.log = log;
        }
        public Frame LogFrame()
        {
            //Create ui elements
            Frame frame = new Frame();
            StackLayout stack = new StackLayout();
            Label Title = new Label();
            Label Date = new Label();
            Label Problem = new Label();
            Label Description = new Label();
            //Add text to the ui elements
            Title.Text = log.Title;
            Date.Text = log.CreationDate.ToString();
            Problem.Text = log.Problem;
            Description.Text = log.Description;
            //Add ui styles
            Title.Style = (Style)Application.Current.Resources["ProfileFrameName"];
            Date.Style = (Style)Application.Current.Resources["ProfileFrameSize"];
            Problem.Style = (Style)Application.Current.Resources["ProfileFrameProblem"];
            Description.Style = (Style)Application.Current.Resources["ProfileFrameProblem"];
            //Add the elements into frames
            Frame TitleFrame = createLabelFrame(Title);
            Frame DateFrame = createLabelFrame(Date);
            Frame ProblemFrame = createLabelFrame(Problem);
            Frame DescriptionFrame = createLabelFrame(Description);
            //Add the frames to the stacklayout
            stack.Children.Add(TitleFrame);
            stack.Children.Add(DateFrame);
            stack.Children.Add(ProblemFrame);
            stack.Children.Add(DescriptionFrame);
            //Add the stacklayout to the main frame
            frame.Content = stack;
            //Return the main frame
            return frame;
        }
    }
}
