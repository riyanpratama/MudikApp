using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudikApp2014.Classes
{
    public class TransitionData
    {
        public static GeoCoordinate start_location { get; set; }
        public static GeoCoordinate finish_location { get; set; }
        public static GeoCoordinate place_location { get; set; }
        public static GeoCoordinate current_location { get; set; }
    }
}
