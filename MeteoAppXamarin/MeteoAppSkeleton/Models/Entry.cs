using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace MeteoAppSkeleton.Models
{
    public class Entry
    {
        public int ID { get;set; }
        public string Name { get;set; }
        public double Long { get; set; }
        public double Lat { get; set; }

        public String Description { get; set; }
        public double Temp { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public string Icon { get; set; }

        public Entry()
        {
        }

        public Entry(string name)
        {
            Name = name;
        }

        public void setEntry(int iD, string name, double Long, double lat, string description, double temp, double maxTemp, double minTemp, string icon)
        {
            Name = name;
            this.Long = Long;
            Lat = lat;
            Description = description;
            Temp = temp;
            MaxTemp = maxTemp;
            MinTemp = minTemp;
            Icon = icon;
        }

        public static implicit operator Entry(Position v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Task<object>(Entry v)
        {
            throw new NotImplementedException();
        }
    }
}