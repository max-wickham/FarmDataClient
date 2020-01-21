using System;
using System.Collections.Generic;
using System.Text;

namespace FarmData.Data
{
    struct FarmInfo
    {
        string Name;
        Tuple<int> Location;
        int Size;
    }
    class Profile
    {
        static List<FarmInfo> FarmProfile = new List<FarmInfo>();
        public static bool UpdateFarmProfile()
        {
            return true;
        }
    }

}
