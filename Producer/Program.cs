using Newtonsoft.Json;
using Resto.Common.Models;
using System.Text;

namespace producer;

public class producer
{
    public static async Task PostMessage(Order postData)
    {
        var json = JsonConvert.SerializeObject(postData);
        var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            using (var client = new HttpClient(httpClientHandler))
            {
                var result = await client.PostAsync("http://RestoApi:5000/api/order", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine("Server returned: " + resultContent);
            }
        }

    }

    static void Main(string[] args)
    {
        Console.WriteLine("Sleeping to wait for Rabbit");
        Task.Delay(10000).Wait();
        Console.WriteLine("Posting messages to webApi");

        var order = new Order()
        {
            Id = 1,
            Email = "test@gmail.com"
        };

        PostMessage(order).Wait();
    }
}
