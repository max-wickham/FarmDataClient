using System;
using System.Collections.Generic;
using System.Text;

namespace FarmData.Data
{
    
    public struct FarmInfo
    {
        public string Name;
        public Tuple<double, double> Location;
        public string Size;//size in acres 
        public FarmInfo(string name, Tuple<double, double> location, string size)
        {
            Name = name;
            Location = location;
            Size = size;
        }
    }
    class Profile
    {
        public static List<string> LiveStocks = new List<string>() { "Cattle", "Sheep" };
        public static List<string> Crops = new List<string>() { "Wheat", "Corn" };
        public static List<string> Sizes = new List<string>() { "1km", "2km" };

        public static List<FarmInfo> FarmProfile = new List<FarmInfo>();
        public static bool UpdateFarmProfile()
        {

            Tuple<double, double> tup = new Tuple<double, double>(1, 2);
            FarmProfile = new List<FarmInfo>();
            FarmInfo farmInfo = new FarmInfo("wheat",tup, "6");
            FarmProfile.Add(farmInfo);
            FarmProfile.Add(farmInfo);
            FarmProfile.Add(farmInfo);
            return true;
        }
        public static bool SaveNewProfile(FarmInfo farmInfo)
        {
            return true;
        }
    }

}
