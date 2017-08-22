using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubConnection = new HubConnection("http://localhost:50205/signalr");
            var h = hubConnection.CreateHubProxy("MyHub");
           
          //  hubConnection.Start().Wait();
            hubConnection.Start().Wait();
            h.On<string>("SendMessage", message => {
                Console.Write(message);

            });
            h.On("SendMessageToClient", message => {
                Console.Write(message);

            });
            if (h != null)
            {
                Console.WriteLine(hubConnection.ConnectionId);
            }
            
            h.Invoke<string>("SendMessage","Hi there").ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }

            }).Wait();
            h.Invoke("SendMessageToClient").ContinueWith(task =>
            {
                Console.WriteLine(task);
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}", task.Exception.GetBaseException());
                }
                else {
                    Console.WriteLine(task.Exception.Message);
                }
         

            }).Wait();
            Console.WriteLine("client says hello to server\n");



            Console.ReadLine();

        }

        private void connectSignalr(string data)
        {
 

           

        }
    }
}
