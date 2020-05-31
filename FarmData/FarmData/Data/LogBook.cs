using FarmData.Models;
using FarmData.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FarmData.Data
{
    public class Log
    {
        public string Title { get; set; }
        public string Problem { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public Log(string title, string problem, string date, string description)
        {
            Title = title;
            Problem = problem;
            CreationDate = date;
            Description = description;
        }
        public Log(string title, string problem,string description)
        {
            Title = title;
            Problem = problem;
            Description = description;
        }
    }
    class LogBook
    {
        private static HTTPHandler handler = new HTTPHandler();
        private static Request request = new Request(handler);

        public static ObservableCollection<Log> LogList = new ObservableCollection<Log>();
        public static async Task<bool> UpdateLogList()
        {
            LogList = new ObservableCollection<Log>();
            //for testing
            /*Log log1 = new Log("Peas", "Mites","12/11/20", "laskhglknv");
            Log log2 = new Log("Peas2", "Mites2", "12/11/20", "laskhglknv");
            LogList.Add(log1);
            LogList.Add(log2);
            */
            string response = await new Request(handler).Post("/getlogs", null, Authentication.Email, Authentication.Password);
            if(response == "error" || response == "invalid") { return false; }
            if (response == "") { return false; }
            else
            {
                Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                foreach (var val in values)
                {
                    Log log = new Log(val.Value["title"], val.Value["problem"], val.Value["date"], val.Value["description"]);
                    LogList.Add(log);
                }
                return true;
            }
        }
        public static async Task<bool> SaveLog(Log log)
        {
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"title",log.Title},
                {"problem",log.Problem},
                {"description",log.Description}
            };
            string response = await new Request(handler).Post("/getcreatelog", data, Authentication.Email, Authentication.Password);
            if (response == "posted")
            {
                LogList.Add(log);
                return true;
            }
            return false;
        }
        public static List<string> WeatherProblems = new List<string>() { Strings.Flood,Strings.Drought,Strings.Frost,Strings.Other};
        public static Dictionary<string, List<string>> PestAndDisease = new Dictionary<string, List<string>>() { { Strings.Corn, new List<string>() {Strings.Fungus,Strings.Other} }};
    }
}
