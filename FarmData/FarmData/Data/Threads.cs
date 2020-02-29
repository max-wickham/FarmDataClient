using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Threads
    {
        static Thread thread1 = new Thread("hello", "max", null, "description", DateTime.Now, 5);
        public static List<Thread> ThreadList = new List<Thread>();

        public static List<Thread> SavedThreads = new List<Thread>();

        public static bool UpdateThreads()
        {
            ThreadList = new List<Thread>();
            ThreadList.Add(thread1);
            ThreadList.Add(thread1);
            ThreadList.Add(thread1);
            ThreadList.Add(thread1);
            ThreadList.Add(thread1);
            ThreadList.Add(thread1);
            return true;
        }
        public static bool UpdateSavedThreadList()
        {
            SavedThreads = new List<Thread>();
            SavedThreads.Add(thread1);
            return true;
        }
        public static bool SaveNewThread(Thread thread)
        {
            UpdateSavedThreadList();
            return true;
        }
        public static bool RemoveSavedThread(Thread thread){
             UpdateSavedThreadList();
            return true;
        }

        public static List<Thread> SearchThreads(string search)
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
        public Thread(string title, string userName, string photoSource, string description, DateTime creationDate, int commentCount)
        {
            //ID = id;
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
