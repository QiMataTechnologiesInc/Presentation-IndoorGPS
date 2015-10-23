using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using QiMata.Indoor.Common;

namespace QiMata.Indoor.WebApp.Controllers
{
    public class BeaconDataController : ApiController
    {
        private Guid _ibeaconClosest;
        private Dictionary<Guid, decimal> _lastKnownPositions = new Dictionary<Guid, decimal>(); 

        public async Task<IHttpActionResult> PostNewBeaconData(IEnumerable<BeaconDistance> beaconData)
        {

            if (beaconData.Any(x => x.Beacon.Uuid == _ibeaconClosest))
            {
                
            }

            return Ok();
        }

        public async Task<IHttpActionResult> PutiBeaconClosest(Guid closedUuid)
        {
            _ibeaconClosest = closedUuid;
            return Ok();
        }
    }
}
