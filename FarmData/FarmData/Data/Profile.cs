using FarmData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FarmData.Data
{
    
    public class FarmInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Tuple<double, double> Location { get; set; }
        public string Size { get; set; }
        public FarmInfo(string name, string type, Tuple<double, double> location, string size)
        {
            Name = name;
            Type = type;
            Location = location;
            Size = size;
        }
    }
    class Profile
    {
        private static HTTPHandler handler = new HTTPHandler();
        private static Request request = new Request(handler);

        public static List<string> LiveStocks = new List<string>() { "Cattle", "Sheep" };
        public static List<string> Crops = new List<string>() { "Wheat", "Corn" };
        public static List<string> Sizes = new List<string>() { "1", "2" };

        public static ObservableCollection<FarmInfo> FarmProfile = new ObservableCollection<FarmInfo>();
        public static async Task<bool> UpdateFarmProfile()
        {
            FarmProfile = new ObservableCollection<FarmInfo>();
            try
            {
                FarmProfile = new ObservableCollection<FarmInfo>();
                string response = await request.Post("/getprofile", null, Authentication.Email, Authentication.Password);
                if (response == "Unauthorized Access")
                {
                    Authentication.AuthenticationError();
                    return false;
                }
                if (response == "invalid" || response == "error")//this needs to be made simpler
                {
                    return false;
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> values = 
                        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    {
                            FarmInfo info = new FarmInfo(val.Value["name"], val.Value["type"],
                                new Tuple<double,double>(double.Parse(val.Value["x"]),double.Parse(val.Value["y"])), 
                                val.Value["size"]);
                            FarmProfile.Add(info);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> SaveNewProfile(FarmInfo farmInfo)
        {
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"name",farmInfo.Name},
                {"type",farmInfo.Type},
                {"x",farmInfo.Location.Item1.ToString()},
                {"y",farmInfo.Location.Item2.ToString()},
                {"size",farmInfo.Size}
            };
            string response = await new Request(handler).Post("/getcreateprofile", data, Authentication.Email, Authentication.Password);
            if (response == "posted")
            {
                FarmProfile.Add(farmInfo);
                return true;
            }
            return false;
        }
    }

}
