using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Models
{
    public class Authentication
    {
        private static HTTPHandler handler = new HTTPHandler();
        //when the application runs the user should be autologged in 
        //using the last username and password and the session key should be stored 
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string UserName { get; set; }

        public const int minPasswordLength = 6;
        public const int maxPasswordLength = 15;
        public static async Task<bool> LogIn(string email, string password)
        {

            //Dictionary<String, String> data = new Dictionary<String, String>()
            //{
            //    {"password",password},
            //    {"username",email}
            //};
            string response = await new Request(handler).Post("/login", null, email, password);
            //DisplayAlert(Strings.Error, Strings.LoginAlert, Strings.Ok);
            if (response == "logged in") {
                Email = email;
                Password = password;
                SaveEmailPassword(email, password);
                return true; 
            }
            return false;
        }
        public static void SaveEmailPassword(string email, string password)
        {
            Email = email;
            Password = password;
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
        public static async Task<bool> UserNameAvailable(string username)
        {
            Dictionary<String,String> data = new Dictionary<String, String>()
            {
                {"username",username }
            };
            string response = await new Request(handler).Post("/usernameavailable", data);
            if (response == "available") { return true; }
            //if(response == "unavailable") { return false; } 
            return false;
        }
        public static async Task<bool> EmailAvailable(string email)
        {
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"email",email }
            };
            string response = await new Request(handler).Post("/emailavailable", data);
            if (response == "available") { return true; }
            //if (response == "unavailable") { return false; }
            return false;
        }
        public static async Task<bool> Register(string email, string password, string username)
        {
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"username", username },
                {"password", password },
                {"email",email }
            };
            string response = await new Request(handler).Post("/register", data);
            if (response == "added user") { return true; }
            return false;
        }
        public static bool LogOut()
        {
            return true;
        }

        public static async void AuthenticationError() {
        //TODO implement
        }

    }
}
