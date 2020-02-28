using System;
using System.Collections.Generic;
using System.Text;

namespace FarmData.Data
{
    public struct Log
    {
        public string Title;
        public string Problem;
        public string Description;
        public DateTime Date;
        public Log(string title, string problem, string description)
        {
            Title = title;
            Problem = problem;
            Date = DateTime.Now;
            Description = description;
        }
    }
    class LogBook
    {
        public static List<Log> LogList = new List<Log>();
        public static bool UpdateLogList()
        {
            LogList = new List<Log>();
            Log log1 = new Log("Peas", "Mites", "laskhglknv");
            Log log2 = new Log("Peas2", "Mites2", "laskhglknv");
            LogList.Add(log1);
            LogList.Add(log2);
            return true;
        }
    }
}
