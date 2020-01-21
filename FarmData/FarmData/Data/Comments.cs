using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Comments
    {
        public static List<Comment> CommentList = new List<Comment>();

        public static bool UpdateComments(int ThreadID)
        {
            //TODO update the comment list for a given thread
            return true;
        }
    }
    class Comment
    {
        int ID;
        int ThreadID;
        string UserName;
        List<Image> Photo;
        string Description;
        DateTime CreationDate;
        Comment(int id, int threadID, string userName, List<Image> photo, string description, DateTime creationDate)
        {
            ID = id;
            ThreadID = threadID;
            UserName = userName;
            Photo = photo;
            Description = description;
            CreationDate = creationDate;
        }

    }
}
