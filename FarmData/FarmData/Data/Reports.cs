using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
        public Crop(string crop, string disease, string warning, string warningColour, string description)
        {
            Type = crop;
            Disease = disease;
            Warning = warning;
            WarningColour = warningColour;
            Description = description;
        }
    }
    public class Weather:Report
    {
        public Weather(string weather, string warning, string warningColour, string description)
        {
            Type = weather;
            Warning = warning;
            WarningColour = warningColour;
            Description = description;
        }
    }
    public class Reports
    {
        //public static List<Crop> cropReportsList = new List<Crop>();
        //public static List<Weather> weatherReportsList = new List<Weather>();
        public static ObservableCollection<Report> ReportList = new ObservableCollection<Report>();

        public static bool UpdateReports()
        {
            //cropReportsList.Add(new Crop("Corn", "Aphids", "High Risk", Color.Yellow, "Based on public data in area and weather data"));
            ReportList.Add(new Crop("Corn2", "Aphid", "High Risk", Color.Yellow.ToHex(), "Based on public data in area and weather data"));
            //ReportList.Add(new Weather("Rain", "High Risk", Color.Yellow.ToHex(), "Based on public data in area and weather data"));

            //return true if able to download crop report from server
            return true;
        }

    }
}
