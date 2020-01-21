using FarmData.Data;
using FarmData.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.ModelsUI
{
    
    public class ReportUI
    {
        public static Frame createLabelFrame(View item)
        {
            Frame frame = new Frame();
            //StackLayout stack = new StackLayout();
            //stack.Children.Add(item);
            frame.Style = (Style)Application.Current.Resources["ReportFrame"];
            frame.Content = item;
            return frame;
        }
    }
    interface ReportUI<T>
    {
        Frame Render();
    }
    public class CropReportUI : ReportUI<CropReportUI>
    {
        //the ui elements 
        Frame frame = new Frame();
        Grid grid = new Grid();
        Label crop = new Label();
        Label disease = new Label();
        Label warning = new Label();
        Button description = new Button();
        bool descriptionHidden;

        //properties
        public string CropType;
        public string DiseaseType;
        public string WarningLevel;
        public Color WarningColour;
        public string Description;

        //setup
        public CropReportUI(CropStruct cropStruct)
        {
            CropType = cropStruct.CropType;
            DiseaseType = cropStruct.Disease;
            WarningLevel = cropStruct.Warning;
            WarningColour = cropStruct.WarningColour;
            Description = cropStruct.Description;
        }

        //create the grid and return it
        public Frame Render()
        {
            
            //set the frame definitions
            frame.Style = (Style)Application.Current.Resources["ReportCropFrame"];
            //set the grid definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Auto) });

            //set the text properties of the ui elements
            //crop.Text = CropType; 
            crop.Text = CropType;
            disease.Text = DiseaseType;           
            warning.Text = WarningLevel;
            warning.TextColor = WarningColour;
            description.Text = Strings.Description;
            description.Clicked += Description_Clicked;

            //set the styles of the ui elements
            crop.Style = (Style)Application.Current.Resources["ReportLabel"];
            disease.Style = (Style)Application.Current.Resources["ReportLabel"];
            warning.Style = (Style)Application.Current.Resources["ReportLabel"];
            description.Style = (Style)Application.Current.Resources["ReportButton"];

            //add the elements to the grid
            Frame cropFrame = ReportUI.createLabelFrame(crop);
            grid.Children.Add(cropFrame, 1, 1);
            Grid.SetColumnSpan(cropFrame, 1);

            Frame diseaseFrame = ReportUI.createLabelFrame(disease);
            grid.Children.Add(diseaseFrame, 1, 2);
            Grid.SetColumnSpan(diseaseFrame, 1);

            Frame warningFrame = ReportUI.createLabelFrame(warning);
            grid.Children.Add(warningFrame, 1, 3);
            Grid.SetColumnSpan(warningFrame, 1);

            Frame descriptionFrame = ReportUI.createLabelFrame(description);
            grid.Children.Add(descriptionFrame, 1, 4);
            Grid.SetColumnSpan(descriptionFrame, 2);


            //add the grid to the frame
            frame.Content = grid;
            return frame;
            
        }

        //toggles the hidding of the description
        private void Description_Clicked(object sender, EventArgs e)
        {
            description.Text = descriptionHidden? Description: Strings.Description;
            descriptionHidden = !descriptionHidden;
        }
    }

    public class WeatherReportUI: ReportUI<WeatherReportUI>
    {
        Frame frame = new Frame();
        Grid grid = new Grid();
        Label weather = new Label();
        Label warning = new Label();
        Button description = new Button();
        bool descriptionHidden;


        public string WeatherType;
        public string WarningLevel;
        public Color WarningColour;
        public string Description;

        public WeatherReportUI(WeatherStruct weatherStruct)
        {
            WeatherType = weatherStruct.WeatherType;
            WarningLevel = weatherStruct.Warning;
            WarningColour = weatherStruct.WarningColour;
            Description = weatherStruct.Description;
        }
        public Frame Render()
        {
            //set the frame definitions
            frame.Style = (Style)Application.Current.Resources["ReportWeatherFrame"];
            //set the grid definitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Auto) });

            //set the text properties of the ui elements
            weather.Text = WeatherType;
            warning.Text = WarningLevel;
            warning.TextColor = WarningColour;
            description.Text = Strings.Description;
            description.Clicked += Description_Clicked;

            //set the styles of the ui elements
            weather.Style = (Style)Application.Current.Resources["ReportLabel"];
            warning.Style = (Style)Application.Current.Resources["ReportLabel"];
            description.Style = (Style)Application.Current.Resources["ReportButton"];

            //add the elements to the grid
            Frame weatherFrame = new Frame();
            weatherFrame.Style = (Style)Application.Current.Resources["ReportFrame"];
            weatherFrame.Content = weather;
            grid.Children.Add(weatherFrame, 1, 1);
            Grid.SetColumnSpan(weatherFrame, 1);

            Frame warningFrame = new Frame();
            warningFrame.Style = (Style)Application.Current.Resources["ReportFrame"];
            warningFrame.Content = warning;
            grid.Children.Add(warningFrame, 1, 2);
            Grid.SetColumnSpan(warningFrame, 1);

            Frame descriptionFrame = new Frame();
            descriptionFrame.Style = (Style)Application.Current.Resources["ReportFrame"];
            descriptionFrame.Content = description;
            grid.Children.Add(descriptionFrame, 1, 3);
            Grid.SetColumnSpan(descriptionFrame, 2);

            //add the grid to the frame
            frame.Content = grid;
            return frame;
        }

        private void Description_Clicked(object sender, EventArgs e)
        {
            description.Text = descriptionHidden ? Description : Strings.Description;
            descriptionHidden = !descriptionHidden;
        }
    }
}
