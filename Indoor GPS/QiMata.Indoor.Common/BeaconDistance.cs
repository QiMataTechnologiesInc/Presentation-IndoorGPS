using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiMata.Indoor.Common
{
    public struct BeaconDistance
    {
        public Beacon Beacon { get; set; }

        public decimal Distance { get; set; }

        public Nullable<Coordinate> GetItemLocaton(List<InitialDistance> distances,Dictionary<Beacon,double> previousDistances)
        {
            var thisBeacon = this;

            var beaconDistances = distances.Where(x => x.Beacon1 == thisBeacon.Beacon || x.Beacon2 == thisBeacon.Beacon);

            if (thisBeacon.Beacon.Coordinate.X == 0)
            {
                if (thisBeacon.Beacon.Coordinate.Y == 0)
                {

                }
                else
                {
                    
                }
            }
            else if (thisBeacon.Beacon.Coordinate.Y == 0)
            {
                
            }
            else
            {
                
            }
            

            return null;
        }

        public double GetPoint(double becaonSeperation, double distanceBeacon1, double distanceBeacon2)
        {
            return (Math.Pow(becaonSeperation, 2) - Math.Pow(distanceBeacon2, 2) + Math.Pow(distanceBeacon1, 2)) /
                (2*becaonSeperation);
        }
    }
}
