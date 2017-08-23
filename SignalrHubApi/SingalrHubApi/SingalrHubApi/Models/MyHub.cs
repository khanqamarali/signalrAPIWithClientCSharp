using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SingalrHubApi.Models
{
    [HubName("MyHub")]
    public class MyHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = String.Format(
                   "{0}: {1}", Context.ConnectionId, message);
            Clients.All.newMessage(message);

        }
        public string SendMessageToClient()
        {
            return "Hi from Server";
        }

        public override Task OnConnected()
        {
            return (base.OnConnected());
        }

        public void SendDataObject(SendData hello)
        {
            Console.WriteLine("Hub hello {0} {1}\n", hello.Id, hello.Data);
            Clients.All.SendDataObject(hello);
        }


        public void SendMessageData(SendData data)
        {
            // process incoming data...
            // transform data...
            // craft new data...

            Clients.All.newData(data);
        }

    }

    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}