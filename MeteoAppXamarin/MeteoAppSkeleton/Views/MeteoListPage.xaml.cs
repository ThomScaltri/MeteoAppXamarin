using System;
using MeteoAppSkeleton.ViewModels;
using Xamarin.Forms;
using Plugin.Geolocator;
using System.Diagnostics;
using Acr.UserDialogs;
using System.Collections;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using MeteoAppSkeleton.Service;

namespace MeteoAppSkeleton.Views
{
    public partial class MeteoListPage : ContentPage
    {

        public MeteoListPage()
        {
            InitializeComponent();
            BindingContext = new MeteoListViewModel();
        }

        async void OnItemAddedAsync(object sender, EventArgs e)
        {

            var pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Default,
                OkText = "Add",
                Title = "Add location"
            }) ;

            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                var location = new Models.Entry();
                location.Name = pResult.Text.ToUpper();

                location = await HttpService.GetWeatherAsync(location.Name);
                    if (location == null)
                    {
                        _ = DisplayAlert("Location Not Found", "", "Close");
                    }

                    else
                    {
                        if(App.Database.IsPresent(location))
                            _ = DisplayAlert("Location Already exist", "", "Close");
                        else
                        {
                            //Adding location on DB
                            _ = App.Database.InsertItemAsync(location);
                            ((MeteoListViewModel)BindingContext).addEntry(location);
                            _ = DisplayAlert("Location Added", "", "Close");
                        }
                    }
            }
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = e.SelectedItem as Models.Entry
                });
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (((MeteoListViewModel)BindingContext).Entries.Count == 1)
            {
                foreach (Models.Entry e in App.Database.GetItemsAsync().Result.ToArray())
                {
                    Models.Entry tmp;
                    tmp = await HttpService.GetWeatherAsync(e.Name);
                    ((MeteoListViewModel)BindingContext).Entries.Add(tmp);
                }
            }
        }
    }
}