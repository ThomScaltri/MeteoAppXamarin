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


        async void GetLocation()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Task<Entry> task = HttpService.GetWeatherAsync(position.Latitude, position.Longitude);
            var gpsloc = await task;
            gpsloc.Name = gpsloc.Name + " (GPS)";
            Entries.RemoveAt(0);
            Entries.Insert(0, gpsloc);
        }
    }
}