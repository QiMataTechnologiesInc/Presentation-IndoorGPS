using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using QiMata.Indoor.Common;

namespace QiMata.Indoor.WebApp.Hubs
{
    public class CoordinateHub : Hub
    {
        public void SendNewCoordinate(Coordinate coordinate)
        {
            Clients.All.NewCoordinate(coordinate);
        }
    }
}
