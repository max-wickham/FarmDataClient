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
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                    //string auth_address = username + ":" + password + "@" + address;
                    var content = new StringContent(jsonify(data), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("http://" + address + page, content);
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

        public static async Task<String> Get(string page = "", string username = "", string password = "")
        {
            string responseString = "";
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //string auth_address = username + ":" + password + "@" + address;         
                var response = await client.GetAsync("http://" + address + page);
                responseString = await response.Content.ReadAsStringAsync();
            }
            return responseString;
        }

        public static string jsonify(Dictionary<String, String> data)
        {
            string response = "{";
            foreach (KeyValuePair<string, string> item in data)
            {
                response += ("\"" + item.Key + "\"");
                response += ":";
                response += ("\"" + item.Value + "\"");
                response += ",";
            }
            if (response != "{") { response = response.Remove(response.Length - 1, 1); }
            response += "}";
            return response;
        }
    }


    /*int timeout = 1000;
var task = SomeOperationAsync();
if (await Task.WhenAny(task, Task.Delay(timeout)) == task) {
    // task completed within timeout
} else { 
    // timeout logic
}*/
}
