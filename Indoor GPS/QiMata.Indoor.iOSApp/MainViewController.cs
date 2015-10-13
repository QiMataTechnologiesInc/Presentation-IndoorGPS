
using System;
using System.Drawing;
using CoreLocation;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace QiMata.Indoor.iOSApp
{
    public partial class MainViewController : UIViewController
    {
        List<CLBeacon>[] beacons;
        CLLocationManager locationManager;
        List<CLBeaconRegion> rangedRegions;

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public MainViewController()
            : base(UserInterfaceIdiomIsPhone ? "MainViewController_iPhone" : "MainViewController_iPad", null)
        {
            locationManager = new CLLocationManager();
            locationManager.DidRangeBeacons += HandleDidRangeBeacons;
        }

        private void HandleDidRangeBeacons(object sender, CLRegionBeaconsRangedEventArgs e)
        {
            foreach (CLBeacon beacon in e.Beacons)
            {
                Console.WriteLine(beacon.Major);
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
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            foreach (CLBeaconRegion region in rangedRegions)
                locationManager.StartRangingBeacons(region);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            foreach (CLBeaconRegion region in rangedRegions)
                locationManager.StopRangingBeacons(region);
        }
    }
}