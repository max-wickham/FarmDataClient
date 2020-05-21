using FarmData.Models;
using FarmData.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Threads
    {
        public static ObservableCollection<Thread> ThreadList = new ObservableCollection<Thread>();
        public static ObservableCollection<Thread> SavedThreads = new ObservableCollection<Thread>();

        //Objects for HTTP Requests
        private static HTTPHandler handler = new HTTPHandler();
        private static Request request = new Request(handler);

        //Updates the thread list
        public static async Task<bool> UpdateThreads()
        {
            try
            {
                ThreadList = new ObservableCollection<Thread>();
                string response = await  request.Post("/getthreadlist",null, Authentication.Email, Authentication.Password);
                if (response == "Unauthorized Access")
                {
                    Authentication.AuthenticationError();
                    return false;
                }
                if (response == "invalid" || response == "")//this needs to be made simpler
                {
                    return false;
                }
                if (response == "empty")//this needs to be made simpler
                {
                    return true;
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    { 
                        Thread thread = new Thread(val.Value["title"], val.Value["username"], null, val.Value["description"], "", 0, Int32.Parse(val.Key));
                        //thread.PhotoSource = "http://" + Request.address + "/getimage/" + val.Key; 
                        ThreadList.Add(thread);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> PostNewThread(Dictionary<string, string> data)
        { 
            string response = await request.Post("/getcreatethread", data, Authentication.Email, Authentication.Password,"");
            if (response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return false;
            }
            return response == "posted";
        }
        public static async Task<string> GetThread(int id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["thread_id"] = id.ToString();
            string response = await request.Post("/getthread", data, Authentication.Email, Authentication.Password);
            if (response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return "error";
            }
            return response;
        }


        public static async Task<bool> UpdateSavedThreadList()
        {
            try
            {
                SavedThreads = new ObservableCollection<Thread>();
                string response = await request.Post("/getsaves",null, Authentication.Email, Authentication.Password);
                if (response == "Unauthorized Access")
                {
                    Authentication.AuthenticationError();
                    return false;
                }
                if (response == "error" || response == "")//this needs to be made simpler
                {
                    return false;
                }
                else
                {
                    Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    {
                        Thread thread = new Thread(val.Value["title"], val.Value["username"], null, val.Value["description"], "", 0, Int32.Parse(val.Key));
                        SavedThreads.Add(thread);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static async Task<bool> SaveNewThread(Thread thread)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["thread_id"] = thread.ID.ToString();
            string response = await request.Post("/getsavethread", data, Authentication.Email, Authentication.Password);
            if(response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return false;
            }   
            if(response == "saved")
            {
                SavedThreads.Add(thread);
                return true;
            }
            return false;
        }
        public static async Task<bool> RemoveSavedThread(Thread thread){
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["thread_id"] = thread.ID.ToString();
            string response = await request.Post("/getunsavethread", data, Authentication.Email, Authentication.Password);
            if (response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return false;
            }
            if (response == "unsaved")
            {
                SavedThreads.Remove(thread);
                return true;
            }
            return false;
        }
        public static ObservableCollection<Thread> SearchThreads(string search)
        {
            //TODO implement    
            return ThreadList;
        }
        public static bool IsSaved(Thread thread)
        {
            foreach (Thread savedThread in SavedThreads)
            {
                if (savedThread.ID == thread.ID)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class Thread
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string PhotoSource { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public int CommentCount { get; set; }
        public Thread(string title, string userName, string photoSource, string description, string creationDate, int commentCount, int id)
        {
            ID = id;
            Title = title;
            UserName = userName;
            PhotoSource = photoSource;
            Description = description;
            CreationDate = creationDate;
            CommentCount = commentCount;
        }
        public Thread(int id, string title, string userName, string photoSource, string description, string creationDate, int commentCount)
        {
            ID = id;
            Title = title;
            UserName = userName;
            PhotoSource = photoSource;
            Description = description;
            CreationDate = creationDate;
            CommentCount = commentCount;
        }

    }
}
