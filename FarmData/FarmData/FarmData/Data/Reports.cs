using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.Data
{
    //TODO needs to contain a list of reports that are downloaded from the server
    public struct CropStruct
    {
        public string CropType;
        public string Disease;
        public string Warning;
        public Color WarningColour;
        public string Description;
        public CropStruct(string crop, string disease, string warning, Color warningColour, string description)
        {
            CropType = crop;
            Disease = disease;
            Warning = warning;
            WarningColour = warningColour;
            Description = description;
        }
    }
    public struct WeatherStruct
    {
        public string WeatherType;
        public string Warning;
        public Color WarningColour;
        public string Description;
        public WeatherStruct(string weather, string warning, Color warningColour, string description)
        {
            WeatherType = weather;
            Warning = warning;
            WarningColour = warningColour;
            Description = description;
        }
    }
    public class Reports
    {
        public static List<CropStruct> cropReportsList = new List<CropStruct>();
        public static List<WeatherStruct> weatherReportsList = new List<WeatherStruct>();

        public static bool UpdateReports()
        {
            cropReportsList.Add(new CropStruct("Corn", "Aphids", "High Risk", Color.Yellow, "Based on public data in area and weather data"));
            cropReportsList.Add(new CropStruct("Corn2", "Aphids", "High Risk", Color.Yellow, "Based on public data in area and weather data"));
            weatherReportsList.Add(new WeatherStruct("Rain", "High Risk", Color.Yellow, "Based on public data in area and weather data"));

            //return true if able to download crop report from server
            return true;
        }

    }
}
