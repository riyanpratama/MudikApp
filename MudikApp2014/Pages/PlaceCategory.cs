using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MudikApp2014.Classes;

namespace MudikApp2014.Pages
{
    public partial class NearbyPlacePage : PhoneApplicationPage
    {
        private List<Item> ResultItems;

        public NearbyPlacePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Title"))
                CategoryName.Text = this.NavigationContext.QueryString["Title"];

            if (this.NavigationContext.QueryString.ContainsKey("category"))
            {
                string category = this.NavigationContext.QueryString["category"];
                GetNearbyPlace(category);
            }            
        }

        private void GetNearbyPlace(string currentCategory)
        {
            if (!App.IsConnected)
            {
                MessageBox.Show(App.NoInternet);
                return;
            }

            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += webClient_DownloadStringCompleted;
            string app_id = "ntNKHCRbu9V1pLvGJbi6";
            string app_code = "LSfBTGOsnK9FR2TemuRHRA";
            string tf = "plain";
            string cat = currentCategory;
            string size = "20";
            string at = TransitionData.current_location.Latitude.ToString().Replace(',', '.') + "," + TransitionData.current_location.Longitude.ToString().Replace(',', '.');
            string url = "http://places.cit.api.here.com/places/v1/discover/explore?";
            url += "at=" + at;
            url += "&app_id=" + app_id ;
            url += "&app_code=" + app_code ;
            url += "&tf=" + tf ;
            url += "&cat=" + cat ;
            url += "&size=" + size ;

            webClient.DownloadStringAsync(new System.Uri(url, UriKind.Absolute));
        }

        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                JsonPlaces result = JsonConvert.DeserializeObject<JsonPlaces>(e.Result);
                NearbyPlaceListBox.ItemsSource = result.results.items.OrderBy(s=>s.distance);
                ResultItems = result.results.items;
                //MessageBox.Show(e.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void NearbyPlaceItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var currTitle = ((sender as Grid).Children[0] as TextBlock).Text;
            var item = ResultItems.Where(s => s.title == currTitle).FirstOrDefault();
            TransitionData.place_location = new System.Device.Location.GeoCoordinate(item.position[0], item.position[1]);
            this.NavigationService.Navigate(new Uri("/Pages/LocationMapPage.xaml?Title=" + item.title,UriKind.Relative));
        }
    }
}