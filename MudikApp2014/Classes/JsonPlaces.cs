using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MudikApp2014.Classes
{
    public class Category
    {
        public string id { get; set; }
        public string title { get; set; }
        public string href { get; set; }
        public string type { get; set; }
        public string system { get; set; }
        public string chaosTamarinField { get; set; }
        public string chaosCapuchinField { get; set; }
        public string chaosDoucField { get; set; }
        public string chaosGibbonField { get; set; }
    }

    public class Item
    {
        public List<double> position { get; set; }
        public int distance { get; set; }
        public string title { get; set; }
        public double averageRating { get; set; }
        public Category category { get; set; }
        public string icon { get; set; }
        public string vicinity { get; set; }
        public List<object> having { get; set; }
        public string type { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public string chaosNihonzaruField { get; set; }
        public string chaosTarsierField { get; set; }
        public string chaosProboscisField { get; set; }
        public string chaosBaboonField { get; set; }
        public string chaosTamarinField { get; set; }
        public string chaosGibbonField { get; set; }
    }

    public class Results
    {
        public string next { get; set; }
        public List<Item> items { get; set; }
    }

    public class Address
    {
        public string text { get; set; }
        public string street { get; set; }
        public string district { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string county { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
    }

    public class Location
    {
        public List<double> position { get; set; }
        public Address address { get; set; }
    }

    public class Context
    {
        public Location location { get; set; }
        public string type { get; set; }
        public string href { get; set; }
        public string chaosTarsierField { get; set; }
    }

    public class Search
    {
        public Context context { get; set; }
    }

    public class JsonPlaces
    {
        public Results results { get; set; }
        public Search search { get; set; }
    }
}
