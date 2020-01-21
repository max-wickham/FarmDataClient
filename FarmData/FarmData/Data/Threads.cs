using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Threads
    {
        static Thread thread1 = new Thread(1, "hello", "max", null, "description", DateTime.Now, 5);
        public static List<Thread> ThreadList = new List<Thread>();

        public static List<int> SavedThreads = new List<int>();

        public static bool UpdateThreads()
        {
            ThreadList.Add(thread1);
            return true;
        }
        public static bool UpdateSavedThreadList()
        {
            return true;
        }
        public static bool SaveNewThread(Thread thread)
        {
            return true;
        }
    }

    public class Thread
    {
        public int ID;
        public string Title;
        public string UserName;
        public List<Image> Photo;
        public string Description;
        public DateTime CreationDate;
        public int CommentCount;
        public Thread(int id, string title, string userName, List<Image> photo, string description, DateTime creationDate, int commentCount)
        {
            ID = id;
            Title = title;
            UserName = userName;
            Photo = photo;
            Description = description;
            CreationDate = creationDate;
            CommentCount = commentCount;
        }
    }
}
