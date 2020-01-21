using System;
using System.Collections.Generic;
using System.Text;

namespace FarmData.Models
{
    public class SessionData
    {
        public static bool isLoggedIn = false;
        public static int clientID;
        public static int sessionKey;

        public static int GenerateClientID()
        {
            return 1;
        }
    }
}
