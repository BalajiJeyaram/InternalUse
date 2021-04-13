using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HelloXamarin.Services;
using HelloXamarin.Views;

namespace HelloXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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
