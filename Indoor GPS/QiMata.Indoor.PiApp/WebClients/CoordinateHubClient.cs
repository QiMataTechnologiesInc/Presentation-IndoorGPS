using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using QiMata.Indoor.Common;

namespace QiMata.Indoor.PiApp.WebClients
{
    public class CoordinateHubClient : IDisposable
    {
        private HubConnection _hubConnection;
        private IHubProxy _hubProxy;

        public CoordinateHubClient(Action<Coordinate> callback)
        {
            _hubConnection = new HubConnection("http://indoorgps.azurewebsites.net");
            _hubProxy = _hubConnection.CreateHubProxy("CoordinateHub");
            _hubProxy.On<Coordinate>("NewCoordinate",callback);
            _hubConnection.Start().Wait();
        }

        public void Dispose()
        {
            _hubConnection?.Dispose();
        }
    }
}
