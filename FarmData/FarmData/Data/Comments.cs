using FarmData.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Data
{
    class Comments
    {
        private static HTTPHandler handler = new HTTPHandler();

        public static ObservableCollection<Comment> CommentList = new ObservableCollection<Comment>();

        public static async Task<bool> UpdateComments(int thread_id)
        {
            //reset the comment list
            CommentList = new ObservableCollection<Comment>();
            //thread id neaded to request the commetn information
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"thread_id",thread_id.ToString()}
            };
            string response = await new Request(handler).Post("/getcomments",data, Authentication.Email, Authentication.Password);
            if (response == ""){return false;}
            else
            {
                Dictionary<string, Dictionary<string, string>> values = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(response);
                foreach (var val in values)
                {
                    Comment comment = new Comment(val.Value["username"], null, val.Value["description"], "");
                    CommentList.Add(comment);
                }
                return true;
            }
        }

        public static async Task<bool> SendComment(Comment comment, int thread_id)
        {
            Dictionary<String, String> data = new Dictionary<String, String>()
            {
                {"thread_id",thread_id.ToString()},
                {"description",comment.Description}
            };
            string response = await new Request(handler).Post("/getcreatecomment", data, Authentication.Email, Authentication.Password);
            if (response == "posted")
            {
                CommentList.Add(comment);
                return true;
            }
            return false;
        }
    }
    public class Comment
    {
        public string UserName { get; set; }
        public string PhotoSource { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public Comment(string userName, string photo, string description, string creationDate)
        {
            UserName = userName;
            PhotoSource = photo;
            Description = description;
            CreationDate = creationDate;
        }
        public Comment(string username, string description)
        {
            UserName = username;
            Description = description;
        }

    }
}
