using System;
using System.Collections.Concurrent;
using System.Drawing;
using CoreLocation;
using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using QiMata.Indoor.Common;

namespace QiMata.Indoor.iOSApp
{
    public partial class MainViewController : UIViewController
    {
        private List<CLBeacon>[] beacons;
        private CLLocationManager locationManager;
        private List<CLBeaconRegion> rangedRegions;
        private Task _uploadTask;
        private ConcurrentQueue<BeaconDistance> _beaconDistances;

        private static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }




        public MainViewController()
            : base(UserInterfaceIdiomIsPhone ? "MainViewController_iPhone" : "MainViewController_iPad", null)
        {
            locationManager = new CLLocationManager();
            locationManager.DidRangeBeacons += HandleDidRangeBeacons;
            _beaconDistances = new ConcurrentQueue<BeaconDistance>();

        }

        private void HandleDidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            foreach (CLBeacon beacon in e.Beacons)
            {
                _beaconDistances.Enqueue(new BeaconDistance
                {
                    Distance = Convert.ToDouble(Math.Pow(10d, (-55 - beacon.Rssi)/20)),
                    Beacon = Defaults.Beacons.Single(x => Guid.Parse(GetGuid(beacon)) == x.Uuid && beacon.Major.Int32Value == x.Major && beacon.Minor.Int32Value == x.Minor)
                });
                Console.WriteLine(
                    $"Distance: {Convert.ToDecimal(Math.Pow(10d, (-55d - beacon.Rssi)/20))} | Major: {beacon.Major} | Minor: {beacon.Minor} | Rssi: {beacon.Rssi}");

            }

            if (_uploadTask == null || _uploadTask.IsCompleted)
            {
                if (!_beaconDistances.IsEmpty)
                {
                    _uploadTask = UploadBeacons();
                }
            }
        }

        private string GetGuid(CLBeacon beacon)
        {
            var notGuid = beacon.ProximityUuid.ToString();
            return notGuid.Substring(notGuid.IndexOf("> ") + 2);
        }

        private async Task UploadBeacons()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://indoorgps.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var response = await client.PostAsJsonAsync("api/BeaconData", _beaconDistances.AsEnumerable());
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            // Populate the regions we will range once.
            rangedRegions = new List<CLBeaconRegion>();
            
            foreach (NSUuid uuid in Defaults.SupportedProximityUuids)
            {
                CLBeaconRegion region = new CLBeaconRegion(uuid, uuid.AsString());
                rangedRegions.Add(region);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://indoorgps.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PutAsJsonAsync("api/BeaconData", new TriangulationRequired
                {
                    InitialDistances = new List<InitialDistance>
                    {
                        new InitialDistance
                        {
                            Beacon1 = 
                        },
                        new InitialDistance
                        {

                        },
                        new InitialDistance
                        {

                        },
                        //new InitialDistance
                        //{

                        //}
                    }
                });
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            foreach (CLBeaconRegion region in rangedRegions)
            {
                locationManager.StartRangingBeacons(region);
            }
        }
        
        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            foreach (CLBeaconRegion region in rangedRegions)
            {
                locationManager.StopRangingBeacons(region);
            }
        }
    }
}
