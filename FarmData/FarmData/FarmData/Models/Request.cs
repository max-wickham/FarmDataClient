using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmData.Models
{
    
    public class Request
    {
        public static string address = "129.31.159.50:5000";

        public static async Task<String> Post(string page = "", Dictionary<String, String> data =  null, string username = "", string password = "")
        {
            try
            {
                string responseString = "";
                using (var client = new HttpClient())
                {
                    string auth_address = username + ":" + password + "@" + address;
                    var content = new StringContent(jsonify(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://" + auth_address + page, content);
                    responseString = await response.Content.ReadAsStringAsync();
                }
                return responseString;
            }
            catch
            {
                //need to imlplement reload here
                return "error";
            }
        }

        public static string jsonify(Dictionary<String,String> data)
        {
            string response = "{";
            foreach (KeyValuePair<string, string> item in data)
            {
                response += ("\"" + item.Key + "\"");
                response += ":";
                response += ("\"" + item.Value + "\"");
                response += ",";
            }
            response = response.Remove(response.Length - 1, 1);
            response += "}";
            return response;
        }
    }
}
