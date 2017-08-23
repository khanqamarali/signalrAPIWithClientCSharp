using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using System.Net.Http;


namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var passData = "";
            Console.WriteLine("EnterMsg");
            passData = Console.ReadLine();
            connectSignalr(passData);
            httpHubWebMethod();

              Console.ReadLine();
        }

      
        private static void  connectSignalr(string msg)
        {            
            var hubConnection = new HubConnection("http://localhost:50205/signalr");
            var hproxy = hubConnection.CreateHubProxy("MyHub");
            hproxy.On<string>("newMessage", message => Console.WriteLine(message));
            hubConnection.Start().Wait();
             hproxy.Invoke<string>("SendMessage", msg).ContinueWith(task =>
             {
                 if (task.IsFaulted)
                 {
                     Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                 }
             }).Wait();
            }

        public static async void httpHubWebMethod()
        {
             HttpClient client = new HttpClient();
            string path = "http://localhost:50205/api/values/5";
            HttpResponseMessage response = await client.GetAsync(path);
            Console.WriteLine(response.ToString());
            Console.ReadLine();
        }



    }
}
