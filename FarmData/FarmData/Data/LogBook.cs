using FarmData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
    }
    class LogBook
    {
        private static HTTPHandler handler = new HTTPHandler();
        private static Request request = new Request(handler);

        public static ObservableCollection<Log> LogList = new ObservableCollection<Log>();
        public static bool UpdateLogList()
        {
            //TODO
            LogList = new ObservableCollection<Log>();
            Log log1 = new Log("Peas", "Mites","12/11/20", "laskhglknv");
            Log log2 = new Log("Peas2", "Mites2", "12/11/20", "laskhglknv");
            LogList.Add(log1);
            LogList.Add(log2);
            return true;
        }
    }
}
