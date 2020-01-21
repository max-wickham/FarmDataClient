using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Models
{
    public class Authentication
    {
        //when the application runs the user should be autologged in 
        //using the last username and password and the session key should be stored 
        public static string email;
        public static string password;

        public const int minPasswordLength = 6;
        public const int maxPasswordLength = 15;
        public static  int LogIn(string email, string password)
        {
            Authentication.email = email;
            Authentication.password = password;
            SaveEmailPassword(email, password);
            //TODO return the SessionKey
            return 0;
        }
        public static void SaveEmailPassword(string email, string password)
        {
            Authentication.email = email;
            Authentication.password = password;
            Application.Current.Properties["email"] = email;
            Application.Current.Properties["password"] = password;
        }
        public static string GetEmail()
        {
            email = Application.Current.Properties["email"] as string;
            return email;
        }

        public static string GetPassword()
        {
            password =  Application.Current.Properties["password"] as string;
            return password;
        }

        public static bool UserNameAvailable(string username)
        {
            //TODO
            return true;
        }
        public static bool EmailAvailable(string email)
        {
            //TODO
            return true;
        }

        public static bool Register(string email, string password, string username)
        {
            //TODO return true if registration successful
            return true;
        }


    }
}
