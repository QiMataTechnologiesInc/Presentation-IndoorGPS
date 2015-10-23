using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using QiMata.Indoor.Common;
using QiMata.Indoor.WebApp.Hubs;

namespace QiMata.Indoor.WebApp.Controllers
{
    public class BeaconDataController : ApiController
    {
        private TriangulationRequired _triangulationRequired;
        private readonly Dictionary<Beacon, double> _lastKnownPositions = new Dictionary<Beacon, double>();

        public IHttpActionResult PostNewBeaconData(IEnumerable<BeaconDistance> beaconData)
        {
            foreach (BeaconDistance beaconDistance in beaconData)
            {
                _lastKnownPositions[beaconDistance.Beacon] = beaconDistance.Distance;
            }
            foreach (BeaconDistance beaconDistance in beaconData)
            {
                var location = beaconDistance.GetItemLocation(_triangulationRequired.InitialDistances, _lastKnownPositions);
                if (location != null)
                {
                    Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<CoordinateHub>()
                        .Clients.All.SendNewCoordinate(location);
                }
            }

            return Ok();
        }

        public IHttpActionResult PutiBeaconClosest(TriangulationRequired triangulationRequired)
        {
            _triangulationRequired = triangulationRequired;
            _lastKnownPositions.Clear();
            foreach (Beacon beacon in _triangulationRequired.InitialDistances.Select(x => x.Beacon1).Union(_triangulationRequired.InitialDistances.Select(x => x.Beacon2)).Distinct())
            {
                _lastKnownPositions[beacon] = 1.414; //sqrt(.5^2 + .5^2);
            }
            return Ok();
        }
    }
}
