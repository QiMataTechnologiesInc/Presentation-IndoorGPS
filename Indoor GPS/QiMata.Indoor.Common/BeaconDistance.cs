using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiMata.Indoor.Common
{
    public struct BeaconDistance
    {
        public Beacon Beacon { get; set; }

        public double Distance { get; set; }

        public Nullable<Coordinate> GetItemLocation(List<InitialDistance> distances,Dictionary<Beacon,double> previousDistances)
        {
            var thisBeacon = this;

            var beaconDistances = distances.Where(x => x.Beacon1 == thisBeacon.Beacon || x.Beacon2 == thisBeacon.Beacon);

            if (previousDistances.Count < 3)
            {
                return null;
            }

            double xCoord = 0;
            double yCoord= 0;

            //if x coord
            if (thisBeacon.Beacon.Coordinate.X == 0)
            {
                var otherXPlane =
                    beaconDistances.FirstOrDefault(
                        x =>
                            (x.Beacon1 == thisBeacon.Beacon && x.Beacon2.Coordinate.X == 1 &&
                             x.Beacon2.Coordinate.Y == thisBeacon.Beacon.Coordinate.Y)
                            ||
                            (x.Beacon2 == thisBeacon.Beacon && x.Beacon1.Coordinate.X == 1 &&
                             x.Beacon1.Coordinate.Y == thisBeacon.Beacon.Coordinate.Y));

                if (otherXPlane != null)
                {
                    var beacon1IsThisBeacon = otherXPlane.Beacon1 == thisBeacon.Beacon;
                    xCoord = GetPoint(otherXPlane.Distance, previousDistances[thisBeacon.Beacon],
                        previousDistances[beacon1IsThisBeacon ? otherXPlane.Beacon2 : otherXPlane.Beacon1]);
                }
            }
            else
            {
                var otherXPlane =
                    beaconDistances.FirstOrDefault(
                        x =>
                            (x.Beacon1 == thisBeacon.Beacon && x.Beacon2.Coordinate.X == 0 &&
                             x.Beacon2.Coordinate.Y == thisBeacon.Beacon.Coordinate.Y)
                            ||
                            (x.Beacon2 == thisBeacon.Beacon && x.Beacon1.Coordinate.X == 0 &&
                             x.Beacon1.Coordinate.Y == thisBeacon.Beacon.Coordinate.Y));

                if (otherXPlane != null)
                {
                    var beacon1IsThisBeacon = otherXPlane.Beacon1 == thisBeacon.Beacon;
                    xCoord = otherXPlane.Distance - GetPoint(otherXPlane.Distance,
                        previousDistances[beacon1IsThisBeacon ? otherXPlane.Beacon2 : otherXPlane.Beacon1],
                        previousDistances[thisBeacon.Beacon]);
                }
            }
            if (thisBeacon.Beacon.Coordinate.Y == 0)
            {
                var otherYPlane =
                    beaconDistances.FirstOrDefault(
                        x =>
                            (x.Beacon1 == thisBeacon.Beacon && x.Beacon2.Coordinate.Y == 1 &&
                             x.Beacon2.Coordinate.X == thisBeacon.Beacon.Coordinate.X)
                            ||
                            (x.Beacon2 == thisBeacon.Beacon && x.Beacon1.Coordinate.Y == 1 &&
                             x.Beacon1.Coordinate.X == thisBeacon.Beacon.Coordinate.X));

                if (otherYPlane != null)
                {
                    var beacon1IsThisBeacon = otherYPlane.Beacon1 == thisBeacon.Beacon;
                    yCoord = GetPoint(otherYPlane.Distance, previousDistances[thisBeacon.Beacon],
                        previousDistances[beacon1IsThisBeacon ? otherYPlane.Beacon2 : otherYPlane.Beacon1]);
                }
            }
            else
            {
                var otherYPlane =
                    beaconDistances.FirstOrDefault(
                        x =>
                            (x.Beacon1 == thisBeacon.Beacon && x.Beacon2.Coordinate.Y == 0 &&
                             x.Beacon2.Coordinate.X == thisBeacon.Beacon.Coordinate.X)
                            ||
                            (x.Beacon2 == thisBeacon.Beacon && x.Beacon1.Coordinate.Y == 0 &&
                             x.Beacon1.Coordinate.X == thisBeacon.Beacon.Coordinate.X));

                if (otherYPlane != null)
                {
                    var beacon1IsThisBeacon = otherYPlane.Beacon1 == thisBeacon.Beacon;
                    yCoord = otherYPlane.Distance - GetPoint(otherYPlane.Distance,
                        previousDistances[beacon1IsThisBeacon ? otherYPlane.Beacon2 : otherYPlane.Beacon1],
                        previousDistances[thisBeacon.Beacon]);
                }
            }

            if (xCoord == 0 || yCoord == 0)
            {
                return null;
            }
            else
            {
                return new Coordinate
                {
                    X = xCoord,
                    Y = yCoord
                };
            }
        }

        public double GetPoint(double becaonSeperation, double distanceBeacon1, double distanceBeacon2)
        {
            return (Math.Pow(becaonSeperation, 2) - Math.Pow(distanceBeacon2, 2) + Math.Pow(distanceBeacon1, 2)) /
                (2*becaonSeperation);
        }
    }
}
