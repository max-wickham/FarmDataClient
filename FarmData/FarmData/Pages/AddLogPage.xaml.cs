using FarmData.Data;
using FarmData.Resources;
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
    public partial class AddLogPage : ContentPage
    {
        public AddLogPage()
        {
            InitializeComponent();
            foreach (FarmInfo info in Profile.FarmProfile.ToList())
            {
                FarmPicker.Items.Add(info.Name);
            }
            ProblemClassPicker.Items.Add(Strings.Weather);
            ProblemClassPicker.Items.Add(Strings.PestDisease);
            DescriptionEntry.Text = Strings.Description;
        }

        private void SetPicker(object sender, EventArgs e)
        {

            int n = FarmPicker.SelectedIndex;
            if (n != -1 && ProblemClassPicker.SelectedIndex != -1)
            {
                if ((String)ProblemClassPicker.SelectedItem == Strings.Weather)
                {
                    ProblemTypePicker.Items.Clear();
                    foreach (String problem in LogBook.WeatherProblems)
                    {
                        ProblemTypePicker.Items.Add(problem);
                    }
                    ProblemTypePicker.IsVisible = true;
                }
                if ((String)ProblemClassPicker.SelectedItem == Strings.PestDisease)
                {
                    ProblemTypePicker.Items.Clear();
                    foreach (String problem in LogBook.PestAndDisease[Profile.FarmProfile.ToList()[n].Type])
                    {
                        ProblemTypePicker.Items.Add(problem);
                    }
                    ProblemTypePicker.IsVisible = true;
                }
            }
            else
            {
                ProblemTypePicker.IsVisible = false;
            }
        }

        async void SaveButton_Clicked(object sender, EventArgs e)
        {
            SaveButton.IsEnabled = false;
            string desc = DescriptionEntry.Text != Strings.Description? DescriptionEntry.Text:"";
            Log log = new Log((String)FarmPicker.SelectedItem, (String)ProblemTypePicker.SelectedItem, desc);
            if(await LogBook.SaveLog(log))
            {
                await Navigation.PushAsync(new LogBookPage());
            }
            SaveButton.IsEnabled = true;
        }


    }
}