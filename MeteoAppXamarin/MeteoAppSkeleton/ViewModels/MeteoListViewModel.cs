using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MeteoAppSkeleton.Models;
using MeteoAppSkeleton.Service;
using Plugin.Geolocator;

namespace MeteoAppSkeleton.ViewModels
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Entry> _entries;

        public ObservableCollection<Entry> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public void addEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Entry>();
            Entry curLocation = new Entry();
            curLocation.ID = 0;
            curLocation.Name = "Current location";

            GetLocation();
            _entries.Add(curLocation);
        }

        /*private async Task<Entry> getGpsWeather()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy=50;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            var entry = await HttpService.GetWeatherAsync(position.Latitude.ToString() , position.Longitude.ToString());
            
            return entry;
        }*/


        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Task<Entry> task = HttpService.GetWeatherAsync(position.Latitude, position.Longitude);
            var gpsloc = await task;
            //gpsloc.Name = "Current location";
            Entries.RemoveAt(0);
            Entries.Insert(0, gpsloc);
        }




    }
}