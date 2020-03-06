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
    public class Report
    {
        public string Type { get; set; }
        public string Warning { get; set; }
        public string WarningColour { get; set; } //hex
        public string Description { get; set; }

    }
    public class Crop:Report
    {
        public string Disease { get; set; }
        public Crop(string crop, string disease, string warning, string description)
        {
            Type = crop;
            Disease = disease;
            Warning = warning;
            WarningColour = "black";
            Description = description;
        }
    }
    public class Weather:Report
    {
        public Weather(string weather, string warning, string description)
        {
            Type = weather;
            Warning = warning;
            WarningColour = "black";
            Description = description;
        }
    }
    public class Reports
    {
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
                string response = await Request.Get("/getreport", Authentication.Email, Authentication.Password);
                if (response == "Unauthorized Access")
                {
                    Authentication.AuthenticationError();
                    return false;
                }
                if (response == "invalid" || response == "")//this needs to be made simpler
                {
                    return false;
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    {
                        if(val.Key == "crop")
                        {
                            Crop crop = new Crop(val.Value["crop"], val.Value["problem"], val.Value["warning"], val.Value["description"]);
                            ReportList.Add(crop);
                        }
                        if(val.Key == "weather")
                        {
                            Weather weather = new Weather(val.Value["weather"], val.Value["warning"], val.Value["description"]);
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
