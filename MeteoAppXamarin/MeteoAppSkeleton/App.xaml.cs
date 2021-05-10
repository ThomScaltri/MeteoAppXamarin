using Acr.UserDialogs;
using MeteoAppSkeleton.Views;
using Xamarin.Forms;

namespace MeteoAppSkeleton
{
    public partial class App : Application
    {
        private static TestDatabase database;

        public static TestDatabase Database
        {
            get
            {
                if (database == null)
                    database = new TestDatabase();
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.LightGreen,
                BarTextColor = Color.White
            };

            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
