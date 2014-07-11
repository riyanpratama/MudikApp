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

namespace MudikApp2014.Pages
{
    public partial class LocationMapPage : PhoneApplicationPage
    {
        public LocationMapPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Title"))
            {
                PlaceName.Text = this.NavigationContext.QueryString["Title"];
                AddMapLayer(TransitionData.place_location, Colors.Green);
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

        private void LocationMapPage_Loaded(object sender, RoutedEventArgs e)
        {
    //        var coord = new GeoCoordinate(52.53, 13.39),
    ////standardMarker = new nokia.maps.map.StandardMarker(coord);
    //marker = new             map.objects.add(standardMarker);
        }

        
    }
}