using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SingalrHubApi.Controllers
{
  
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            string mymessage = "";
            var hubConnection = new HubConnection("http://localhost:50205/signalr");
            var h = hubConnection.CreateHubProxy("MyHub");
            h.On<string>("newMessage", message => {
                mymessage = message;
            });
            hubConnection.Start().Wait();
            return mymessage;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
