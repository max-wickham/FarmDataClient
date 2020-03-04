﻿using FarmData.Models;
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
        //static Thread thread1 = new Thread("hello", "max", null, "description", DateTime.Now, 5);
        //public static List<Thread> ThreadList = new List<Thread>();
        public static ObservableCollection<Thread> ThreadList = new ObservableCollection<Thread>();
        //public static List<Thread> SavedThreads = new List<Thread>();
        public static ObservableCollection<Thread> SavedThreads = new ObservableCollection<Thread>();
        public static async Task<bool> UpdateThreads()
        {
            try
            {
                ThreadList = new ObservableCollection<Thread>();
                string response = await Request.Get("/getthreadlist", Authentication.Email, Authentication.Password);
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
                    Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                    foreach (var val in values)
                    { 
                        Thread thread = new Thread(val.Value["title"], val.Value["username"], null, val.Value["description"], DateTime.Now,0, Int32.Parse(val.Key));
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
        public static async Task<bool> PostNewThread(string title, string description)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["title"] = title;
            data["description"] = description;
            string response = await Request.Post("/getcreatethread", data, Authentication.Email, Authentication.Password);
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
            string response = await Request.Post("/getthread", data, Authentication.Email, Authentication.Password);
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
                string response = await Request.Get("/getsaves", Authentication.Email, Authentication.Password);
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
                        Thread thread = new Thread(val.Value["title"], val.Value["username"], null, val.Value["description"], DateTime.Now, 0, Int32.Parse(val.Key));
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
            string response = await Request.Post("/getsavethread", data, Authentication.Email, Authentication.Password);
            if(response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return false;
            }   
            if(response == "saved")
            {
                SavedThreads.Add(thread);
            }
            return false;
        }
        public static async Task<bool> RemoveSavedThread(Thread thread){
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["thread_id"] = thread.ID.ToString();
            string response = await Request.Post("/getunsavethread", data, Authentication.Email, Authentication.Password);
            if (response == "Unauthorized Access")
            {
                Authentication.AuthenticationError();
                return false;
            }
            if (response == "unsaved")
            {
                SavedThreads.Remove(thread);
            }
            return false;
        }
        public static ObservableCollection<Thread> SearchThreads(string search)
        {
            //if(search == "")
           // {
                return ThreadList;
            //}
           // return new List<Thread>();
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
        public DateTime CreationDate { get; set; }
        public int CommentCount { get; set; }
        public Thread(string title, string userName, string photoSource, string description, DateTime creationDate, int commentCount, int id)
        {
            ID = id;
            Title = title;
            UserName = userName;
            PhotoSource = photoSource;
            Description = description;
            CreationDate = creationDate;
            CommentCount = commentCount;
        }
        public Thread(int id, string title, string userName, string photoSource, string description, DateTime creationDate, int commentCount)
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
