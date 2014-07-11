using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using MudikApp2014.Classes;
using Microsoft.Phone.Maps.Services;

namespace MudikApp2014.Pages
{
    public partial class LocationMapPage : PhoneApplicationPage
    {
        public LocationMapPage()
        {
            InitializeComponent();
            geoQ = new RouteQuery();
            geoQ.QueryCompleted += geoQ_QueryCompleted;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Title"))
            {
                PlaceName.Text = this.NavigationContext.QueryString["Title"];
                GetRoute(TransitionData.current_location, TransitionData.place_location);
                AddMapLayer(TransitionData.current_location, Colors.Green);
                AddMapLayer(TransitionData.place_location, Colors.Blue);
            }
        }

        private void AddMapLayer(GeoCoordinate geo, Color color)
        {
            map1.Layers.Add(new MapLayer()
            {
                new MapOverlay()
                {
                    GeoCoordinate = geo,
                    PositionOrigin = new Point(0.5,0.5),
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(color),
                        Stroke = new SolidColorBrush(Colors.Red),
                        StrokeThickness = 3,
                        Width = 20,
                        Height = 30
                    }
                    
                }
            });

            map1.Center = geo;
        }

        #region GetRoute
        private List<GeoCoordinate> _resultOfPoint;
        private RouteQuery geoQ;
        private MapRoute LastRutte = null;

        private void GetRoute(GeoCoordinate start, GeoCoordinate finish)
        {
            _resultOfPoint = new List<GeoCoordinate>();
            _resultOfPoint.Add(start);
            _resultOfPoint.Add(finish);

            // Check
            if (geoQ.IsBusy == true)
            {
                geoQ.CancelAsync();
            }

            geoQ.RouteOptimization = RouteOptimization.MinimizeDistance;
            geoQ.TravelMode = TravelMode.Driving;
            geoQ.Waypoints = _resultOfPoint;
            geoQ.QueryAsync();
        }


        void geoQ_QueryCompleted(object sender, QueryCompletedEventArgs<Microsoft.Phone.Maps.Services.Route> e)
        {

            if (LastRutte != null)
            {
                map1.RemoveRoute(LastRutte);
                LastRutte = null;
            }

            try
            {
                Microsoft.Phone.Maps.Services.Route myRutte = e.Result;
                LastRutte = new MapRoute(myRutte);

                map1.AddRoute(LastRutte);
                map1.SetView(e.Result.BoundingBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void LocationMapPage_Loaded(object sender, RoutedEventArgs e)
        {
    //        var coord = new GeoCoordinate(52.53, 13.39),
    ////standardMarker = new nokia.maps.map.StandardMarker(coord);
    //marker = new             map.objects.add(standardMarker);
        }

        
    }
}