using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RitAutomationTestTask1.Domain.Utils
{
    public static class GMapUtils
    {
        public static GMarkerGoogle CreateMarker(Guid id, string name, double lat, double lng, GMarkerGoogleType type = GMarkerGoogleType.purple)
        {
            var marker = new GMarkerGoogle(new PointLatLng(lat, lng), type);
            marker.Tag = id;
            marker.ToolTipText = name;

            return marker;
        }
    }
}
