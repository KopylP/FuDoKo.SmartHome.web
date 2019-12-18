using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FuDoKo.SmartHome.web.Firebase
{
    public class FudokoCloudMessage
    {
        private string serverKey;
        private string firebaseCloudmessagingUrl;
        public FudokoCloudMessage(IConfiguration configuration)
        {
            firebaseCloudmessagingUrl = configuration["Firebase:Url"];
            serverKey = configuration["Firebase:ServerKey"];
        }

        public async Task Push(string title, string body, object data, params string[] tokens)
        {
            var message = new Message
            {
                notification = new Notification
                {
                    text = body,
                    title = title
                },
                registration_ids = tokens,
                data = data
            };
            string jsonMessage = JsonConvert.SerializeObject(message);

            var request = new HttpRequestMessage(HttpMethod.Post, firebaseCloudmessagingUrl);
            request.Headers.TryAddWithoutValidation("Authorization", "key =" + serverKey);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request).ConfigureAwait(false);
            }
        }
    }
}
