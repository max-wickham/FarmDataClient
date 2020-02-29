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
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string UserName { get; set; }

        public const int minPasswordLength = 6;
        public const int maxPasswordLength = 15;
        public static  int LogIn(string email, string password)
        {
            Authentication.Email = email;
            Authentication.Password = password;
            SaveEmailPassword(email, password);
            //TODO return the SessionKey
            return 0;
        }
        public static void SaveEmailPassword(string email, string password)
        {
            Authentication.Email = email;
            Authentication.Password = password;
            Application.Current.Properties["email"] = email;
            Application.Current.Properties["password"] = password;
        }
        public static string GetEmail()
        { 
            try { Email = Application.Current.Properties["email"] as string; }            
            catch { Email = ""; }   
            return Email;
        }

        public static string GetPassword()
        {
            try { Password = Application.Current.Properties["password"] as string; }
            catch { Password = ""; }
            return Password;
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

        public static bool LogOut()
        {
            return true;
        }

    }
}
