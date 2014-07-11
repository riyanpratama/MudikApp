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
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Text;
using Windows.Phone.Speech.Synthesis;
using System.Diagnostics;
using MudikApp2014.Classes;

namespace MudikApp2014.Pages
{
    public partial class GetDirectionPage : PhoneApplicationPage
    {
        private GeoCoordinate geo1;
        private GeoCoordinate geo2;
        private List<RouteManeuver> routePoints;
        private int routePoint = 0;
        private MapRoute mapRoute;
        private string currentAddress = "";
        private List<Directions> directions { get; set; }

        // Constructor
        public GetDirectionPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (!App.IsConnected)
            {
                MessageBox.Show(App.NoInternet);
                return;
            }

            map.Center = TransitionData.start_location;
            map.ZoomLevel = 13;
            // remove possible previous one layer
            if (map.Layers.Count() > 0) map.Layers.Clear();
            AddMapLayer(TransitionData.start_location, Colors.Blue);
            AddMapLayer(TransitionData.finish_location, Colors.Red);
            FindRoute();
        }

        private void AddMapLayer(GeoCoordinate geo, Color color)
        {
            map.Layers.Add(new MapLayer()
            {
                new MapOverlay()
                {
                    GeoCoordinate = geo,
                    PositionOrigin = new Point(0.5,0.5),
                    Content = new Ellipse
                    {
                        Fill = new SolidColorBrush(color),
                        Width = 20,
                        Height = 20
                    }
                }
            });
        }

        private void FindRoute()
        {
            RouteQuery query = new RouteQuery()
            {
                TravelMode = TravelMode.Driving,
                Waypoints = new List<GeoCoordinate>()
                {
                    TransitionData.start_location, 
                    TransitionData.finish_location, 
                }
            };
            query.QueryCompleted += routeQuery_QueryCompleted;
            query.QueryAsync();
        }

        private void ShowRoutePointInfo() 
        {
            // location
            directions = new List<Directions>();
            GeoCoordinate geoCoordinate = routePoints.ElementAt(routePoint).StartGeoCoordinate;
            foreach (var item in routePoints)
            {
                //GeoCoordinate geoCoordinate = routePoints.ElementAt(routePoint).StartGeoCoordinate;
                directions.Add(new Directions("- " + item.InstructionText + " " + item.LengthInMeters + " m"));
            }
            DirectionListBox.ItemsSource = this.directions;
            //directionTextBlock.Text = sb.ToString();
            // delete previous one
            if (map.Layers.Count() > 2) map.Layers.RemoveAt(2);
            // show a new one
            AddMapLayer(geoCoordinate, Colors.Green);
            // center to this location
            map.Center = geoCoordinate;
            
        }
        
        void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Microsoft.Phone.Maps.Services.Route> e)
        {
            if (mapRoute != null) map.RemoveRoute(mapRoute);
            routePoint = 0;
            mapRoute = new MapRoute(e.Result);
            map.AddRoute(mapRoute);
            map.SetView(e.Result.BoundingBox);
            //mainInfoTextBlock.Text = "Distance: " + e.Result.LengthInMeters + " Time: " + e.Result.EstimatedDuration;
            
            routePoints = e.Result.Legs.SelectMany(l => l.Maneuvers).ToList();
            ShowRoutePointInfo();
        }
    }

    public class Directions
    {
        public string direction { get; set; }
        public Directions(string _direction)
        {
            direction = _direction;
        }
    }
}