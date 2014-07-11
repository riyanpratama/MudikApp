using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MudikApp2014.Classes;
using Newtonsoft.Json;
using System.Device.Location;
using Microsoft.Phone.Maps.Services;
using System.Diagnostics;
using Microsoft.Phone.Maps.Controls;

namespace MudikApp2014.Pages
{
    public partial class GeneralRoutePage : PhoneApplicationPage
    {
        private List<string> _resultOfPlace;
        private List<GeoCoordinate> _resultOfPoint;
        private Dictionary<string, List<GeoCoordinate>> ListOfResultLocations;
        private RouteQuery geoQ;
        private MapRoute LastRutte = null;

        public GeneralRoutePage()
        {
            InitializeComponent();            
            geoQ = new RouteQuery();
            geoQ.QueryCompleted += geoQ_QueryCompleted;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("QueryJalur"))
            {
                string QueryJalur = this.NavigationContext.QueryString["QueryJalur"];
                JalurName.Text = QueryJalur.Insert(5," ");
                if(QueryJalur == "JalurUtara")
                    GetListOfLocations(AppDataConstant.JalurUtara);
                else if (QueryJalur == "JalurTengah")
                    GetListOfLocations(AppDataConstant.JalurTengah);
                else
                    GetListOfLocations(AppDataConstant.JalurSelatan);
            } 
        }

        private void GetListOfLocations(string contentJalur)
        {
            _resultOfPoint = new List<GeoCoordinate>();
            RootObjectJalur result = JsonConvert.DeserializeObject<RootObjectJalur>(contentJalur);
            bool IsVeryBeginning = true;
            foreach (var leg in result.routes[0].legs)
            {
                if (IsVeryBeginning)
                {
                    _resultOfPoint.Add(new GeoCoordinate(leg.start_location.lat, leg.start_location.lng));
                    IsVeryBeginning = false;
                }
                _resultOfPoint.Add(new GeoCoordinate(leg.end_location.lat, leg.end_location.lng));

                // CODE BELOW IS CORRECT BUT NOT QUICK
                //if (IsVeryBeginning)
                //{
                //    _resultOfPoint.Add(new GeoCoordinate(leg.start_location.lat, leg.start_location.lng));
                //    IsVeryBeginning = false;
                //}
                ////Debug.WriteLine(leg.start_address + " to " + leg.end_address);
                //foreach (var step in leg.steps)
                //{
                //    _resultOfPoint.Add(new GeoCoordinate(step.end_location.lat, step.end_location.lng));
                //}
            }
        }

        #region GetRoute
        private void GetRoute()
        {
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
            Debug.WriteLine("Route query, error: " + e.Error);
            Debug.WriteLine("Route query, cancelled: " + e.Cancelled);
            Debug.WriteLine("Route query, cancelled: " + e.UserState);
            
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
            SetProgressIndicator(false);

        }
        #endregion

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.IsConnected)
            {
                MessageBox.Show(App.NoInternet);
                return;
            }
            SetProgressIndicator(true);
            GetRoute();
        }

        private void SetProgressIndicator(bool IsLoading)
        {
            if (IsLoading)
            {
                map1.Visibility = Visibility.Collapsed;
            }
            else
            {
                map1.Visibility = Visibility.Visible;
            }
        }
    }
}