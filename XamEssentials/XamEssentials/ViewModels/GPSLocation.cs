using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamEssentials.ViewModels
{
    public class GPSLocation : BindableBase
    {
        public GPSLocation()
        {
            longitude = "???";
            latitude = "???";
            altitude = "???";
        }

        string longitude;
        public string Longitude
        {
            get { return longitude; }
            set { SetProperty(ref longitude, value); }
        }

        string latitude;
        public string Latitude
        {
            get { return latitude; }
            set { SetProperty(ref latitude, value); }
        }

        string altitude;
        public string Altitude
        {
            get { return altitude; }
            set { SetProperty(ref altitude, value); }
        }
    }
}
