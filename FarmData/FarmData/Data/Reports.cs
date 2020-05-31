using FarmData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Data
{
    //TODO needs to contain a list of reports that are downloaded from the server
    public abstract class Report
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Warning { get; set; }
        public string WarningColour { get; set; } //hex
        public string Description { get; set; }

    }
    public class Crop:Report
    {
        public Crop(string problem, string title, string warning, string description)
        {
            Type = problem;
            Title = title;
            Warning = warning;
            Description = description;
        }
    }
    public class LiveStock : Report
    {
        public string Disease { get; set; }
        public LiveStock(string problem, string title, string warning, string description)
        {
            Title = title;
            Type = problem;
            Warning = warning;
            Description = description;
        }
    }
    public class Weather:Report
    {
        public Weather(string problem, string title, string warning, string description)
        {
            Type = problem;
            Warning = warning;
            Title = title;
            Description = description;
        }
    }
    public class Reports
    {
        private static HTTPHandler handler = new HTTPHandler();
        private static Request request = new Request(handler);
        //public static List<Crop> cropReportsList = new List<Crop>();
        //public static List<Weather> weatherReportsList = new List<Weather>();
        public static ObservableCollection<Report> ReportList = new ObservableCollection<Report>();
        
        public static async Task<bool> UpdateReports()
        {
            ReportList.Add(new Crop("Corn2", "Aphid", "High Risk", "Based on public data in area and weather data"));
            try
            {
                ReportList = new ObservableCollection<Report>();
                //string response = "";
                string response = await request.Post("/getreports",null, Authentication.Email, Authentication.Password);
                if (response == "Unauthorized Access")
                {
                    Authentication.AuthenticationError();
                    return false;
                }
                if (response == "invalid")//this needs to be made simpler
                {
                    return false;
                }
                if(response == "empty")
                {
                    return true;
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    {
                        if(val.Key.Split(',')[0] == "crop")
                        {
                            Crop crop = new Crop(val.Value["problem"], val.Value["title"], val.Value["warning"], val.Value["description"]);
                            ReportList.Add(crop);
                        }
                        if (val.Key.Split(',')[0] == "livestock")
                        {
                            LiveStock livestock = new LiveStock(val.Value["problem"], val.Value["title"], val.Value["warning"], val.Value["description"]);
                            ReportList.Add(livestock);
                        }
                        if (val.Key.Split(',')[0] == "weather")
                        {
                            Weather weather = new Weather(val.Value["problem"], val.Value["title"], val.Value["warning"], val.Value["description"]);
                            ReportList.Add(weather);
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
