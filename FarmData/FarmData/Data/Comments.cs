using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Comments
    {
        public static List<Comment> CommentList = new List<Comment>();

        public static bool UpdateComments(Thread thread)
        {
            CommentList = new List<Comment>();
            for(int i = 0; i < 6; i++)
            {
                Comment comment = new Comment("max" + i.ToString(), null, "once upon a time there was a boy named harry", DateTime.Now);
                CommentList.Add(comment);
            }
            
            //TODO update the comment list for a given thread
            return true;
        }

        public static bool SendComment(Comment comment, Thread thread)
        {
            CommentList.Add(comment);
            return true;
        }
    }
    class Comment
    {
       // public int ID { get; set; }
        //public int ThreadID { get; set; }
        public string UserName { get; set; }
        public string PhotoSource { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public Comment(string userName, string photo, string description, DateTime creationDate)
        {
            UserName = userName;
            PhotoSource = photo;
            Description = description;
            CreationDate = creationDate;
        }
        public Comment(string photo, string description)
        {
            PhotoSource = photo;
            Description = description;
        }

    }
}
