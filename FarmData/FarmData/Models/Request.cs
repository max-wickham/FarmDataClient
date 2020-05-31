using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FarmData.Models
{

    public class Request
    {
        //public static string address = "FarmData.us-west-2.elasticbeanstalk.com";
        public static string address = "192.168.1.77:5000";
        private HTTPHandler handler { get; set; }
        public Request(HTTPHandler handler)
        {
            this.handler = handler;
        }
        public async Task<String> Post(string page = "", Dictionary<String, String> data = null, string username = "", string password = "", string imagePath = "")
        {
            if (data == null) { data = new Dictionary<string, string>(); }
            try
            {

                string responseString = "";
                Dictionary<String, String> transmit = data;
                transmit.Add("username", username);
                transmit.Add("password", password);
                //setup request authorisation
                //var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
                //handler.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //setup request content
                var content = new StringContent(jsonify(transmit), Encoding.UTF8, "application/json");
                //setup request image


                //send request and get reply
                var response = await handler.client.PostAsync("http://" + address + page, content);
                //var response = await handler.client.PostAsync("http://" + address + page, form);
                responseString = await response.Content.ReadAsStringAsync();
                return responseString;

            }
            catch
            {
                //need to imlplement reload here
                return "error";
            }

        }

        public async Task<String> Get(string page = "", string username = "", string password = "")
        {
            string responseString = "";

            //client.Timeout = TimeSpan.FromSeconds(10);
            var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
            handler.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //string auth_address = username + ":" + password + "@" + address;         
            var response = await handler.client.GetAsync("http://" + address + page);
            responseString = await response.Content.ReadAsStringAsync();
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



        public async Task<String> PostTest(string page = "", Dictionary<String, String> input = null, string username = "", string password = "", string imagePath = "")
        {
            Dictionary<String, String> data = input;

            if (data == null) { data = new Dictionary<string, string>(); }
            try
            {

                string responseString = "";

                //setup request authorisation
                var byteArray = Encoding.ASCII.GetBytes(username + ":" + password);
                handler.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                //setup request content
                //------->var content = new StringContent(jsonify(data), Encoding.UTF8, "application/json");
                //MultipartFormDataContent form = new MultipartFormDataContent();
                //HttpContent json = new StringContent(jsonify(data), Encoding.UTF8, "application/json");
                if (imagePath != "")
                {
                    byte[] imageBinary = File.ReadAllBytes(imagePath);
                    string image = Convert.ToBase64String(imageBinary, 0, imageBinary.Length);//
                    data["image"] = image;
                    //HttpContent image = new StreamContent(new MemoryStream(imageBinary));
                    //form.Add(image, "image", "upload.jpg");
                }
                var form = new StringContent(jsonify(data), Encoding.UTF8, "application/json");//
                                                                                               //form.Add(json, "json");
                                                                                               //setup request image


                //send request and get reply
                //------->var response = await handler.client.PostAsync("http://" + address + page, content);
                var response = await handler.client.PostAsync("http://" + address + page, form);
                responseString = await response.Content.ReadAsStringAsync();
                return responseString;

            }
            catch
            {
                //need to imlplement reload here
                return "error";
            }

        }


    }
    public class HTTPHandler
    {
        public HttpClient client { get; private set; }
        public HTTPHandler()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            client.MaxResponseContentBufferSize = 1000000;
            ServicePointManager.DefaultConnectionLimit = 4;
        }
    }
}


    

    /*int timeout = 1000;
var task = SomeOperationAsync();
if (await Task.WhenAny(task, Task.Delay(timeout)) == task) {
    // task completed within timeout
} else { 
    // timeout logic
}*/