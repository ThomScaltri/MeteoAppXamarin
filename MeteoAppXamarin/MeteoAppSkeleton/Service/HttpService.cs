using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using MeteoAppSkeleton.Views;

namespace MeteoAppSkeleton.Service
{
    public class HttpService
    {
        public HttpService(){}

        //Method to get info from name
        public static async Task<Models.Entry> GetWeatherAsync(string name)
        {
            MeteoListPage mlp = new MeteoListPage();
            var httpClient = new HttpClient();
            var content = "null";
            try {
                content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + name + "&units=metric&appid=9c7bc41f67eb76922b7785d7a39546ec");
                var location = new Models.Entry
                {
                    Name = (string)JObject.Parse(content)["name"],
                    Lat = (double)JObject.Parse(content)["coord"]["lat"],
                    Long = (double)JObject.Parse(content)["coord"]["lon"],
                    Description = (string)JObject.Parse(content)["weather"][0]["description"],
                    Temp = (double)JObject.Parse(content)["main"]["temp"],
                    MaxTemp = (double)JObject.Parse(content)["main"]["temp_max"],
                    MinTemp = (double)JObject.Parse(content)["main"]["temp_min"],
                    Icon = "http://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png",

                };
                return location;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
                _=mlp.DisplayAlert("Location Not Found", "", "Close");
            }

            return null;
        }

        //Method to get info from coord
        public static async Task<Models.Entry> GetWeatherAsync(double lati,double longi)
        {

            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lat=" + lati + "&lon=" + longi + "&units=metric&appid=9c7bc41f67eb76922b7785d7a39546ec");

            var location = new Models.Entry
            {
                Name = (string)JObject.Parse(content)["name"],
                Lat = (double)JObject.Parse(content)["coord"]["lat"],
                Long = (double)JObject.Parse(content)["coord"]["lon"],
                Description = (string)JObject.Parse(content)["weather"][0]["description"],
                Temp = (double)JObject.Parse(content)["main"]["temp"],
                MaxTemp = (double)JObject.Parse(content)["main"]["temp_max"],
                MinTemp = (double)JObject.Parse(content)["main"]["temp_min"],
                Icon = "http://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png",

            };

            return location;
        }

    }
}
