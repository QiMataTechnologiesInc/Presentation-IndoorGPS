using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using QiMata.Indoor.Common;

namespace QiMata.Indoor.iOSApp
{
    static class Defaults
    {
        static NSUuid[] supportedProximityUuids;
        static NSNumber defaultPower;

        static Defaults()
        {
            supportedProximityUuids = new NSUuid[] {
                new NSUuid ("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0"),
                new NSUuid ("5A4BCFCE-174E-4BAC-A814-092E77F6B7E5"),
                new NSUuid ("74278BDA-B644-4520-8F0C-720EAF059935"),
                new NSUuid("e20a39f4-73f5-4bc4-a12f-17d1ad07a961"), 
                new NSUuid("636f3f8f-6491-4bee-95f7-d8cc64a863b5"), 
            };
            defaultPower = new NSNumber(-59);
        }

        static public string Identifier
        {
            get { return "com.apple.AirLocate"; }
        }

        static public NSUuid DefaultProximityUuid
        {
            get { return supportedProximityUuids[0]; }
        }

        static public IList<NSUuid> SupportedProximityUuids
        {
            get { return supportedProximityUuids; }
        }

        static public NSNumber DefaultPower
        {
            get { return defaultPower; }
        }

        public static List<Beacon> Beacons = new List<Beacon>
        {
            new Beacon
            {
                Uuid = Guid.Parse("E2C56DB5-DFFB-48D2-B060-D0F5A71096E0"),
                Major = 0,
                Minor = 0,
                Coordinate = new Coordinate {X=0, Y=0 }
            },
            new Beacon
            {
                Uuid = Guid.Parse("e20a39f4-73f5-4bc4-a12f-17d1ad07a961"),
                Major = 0,
                Minor = 2,
                Coordinate = new Coordinate {X=1, Y=0 }
            },
            new Beacon
            {
                Uuid = Guid.Parse("636f3f8f-6491-4bee-95f7-d8cc64a863b5"),
                Major = 0,
                Minor = 1,
                Coordinate = new Coordinate {X=0, Y=1 }
            },
            //new Beacon
            //{

            //}
        };
    }
}
