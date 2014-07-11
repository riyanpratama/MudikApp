using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Maps.Services;
using System.Device.Location;

namespace MudikApp2014.Pages
{
    public partial class TestingPage : PhoneApplicationPage
    {
        public TestingPage()
        {
            InitializeComponent();
            SearchForTerm("Batunung");
        }
        private void SearchForTerm(String searchTerm)
        {
            var MyGeocodeQuery = new GeocodeQuery();
            MyGeocodeQuery.SearchTerm = searchTerm;
            MyGeocodeQuery.GeoCoordinate = new GeoCoordinate(-6.9148640, 107.6082420);
            MyGeocodeQuery.QueryCompleted += GeocodeQuery_QueryCompleted;
            MyGeocodeQuery.QueryAsync();
        }

        private void GeocodeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<IList<MapLocation>> e)
        {
            if (e.Error == null)
            {
                //if (e.Result.Count > 0)
                //{
                //    if (_isRouteSearch) // Query is made to locate the destination of a route
                //    {
                //    }
                //    else // Query is made to search the map for a keyword
                //    {
                //        // Add all results to MyCoordinates for drawing the map markers
                //        for (int i = 0; i < e.Result.Count; i++)
                //        {
                //            MyCoordinates.Add(e.Result[i].GeoCoordinate);
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No match found. Narrow your search e.g. Seattle WA.");
                //}
            }
        }
    }
}