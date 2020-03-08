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
        public Tuple<double, double> Location { get; set; }
        public string Size { get; set; }//size in acres 
        public FarmInfo(string name, Tuple<double, double> location, string size)
        {
            Name = name;
            Location = location;
            Size = size;
        }
    }
    class Profile
    {
        private static HTTPHandler handler = new HTTPHandler();

        public static List<string> LiveStocks = new List<string>() { "Cattle", "Sheep" };
        public static List<string> Crops = new List<string>() { "Wheat", "Corn" };
        public static List<string> Sizes = new List<string>() { "1km", "2km" };

        public static ObservableCollection<FarmInfo> FarmProfile = new ObservableCollection<FarmInfo>();
        public static async Task<bool> UpdateFarmProfile()
        {
            Tuple<double, double> tup = new Tuple<double, double>(1, 2);
            FarmProfile = new ObservableCollection<FarmInfo>();
            FarmInfo farmInfo = new FarmInfo("wheat",tup, "6");
            FarmProfile.Add(farmInfo);
            FarmProfile.Add(farmInfo);
            FarmProfile.Add(farmInfo);
            //return true;

            try
            {
                FarmProfile = new ObservableCollection<FarmInfo>();
                //string response = "";
                string response = await new Request(handler).Get("/getprofile", Authentication.Email, Authentication.Password);
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
                    Dictionary<string, Dictionary<string, string>> values = 
                        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    {
                            FarmInfo info = new FarmInfo(val.Value["name"],
                                new Tuple<double,double>(Int32.Parse(val.Value["x"]),Int32.Parse(val.Value["y"])), 
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
        public static bool SaveNewProfile(FarmInfo farmInfo)
        {
            return true;
        }
    }

}
