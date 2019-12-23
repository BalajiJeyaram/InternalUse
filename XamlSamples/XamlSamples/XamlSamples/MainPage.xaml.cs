
using Xamarin.Forms;

namespace XamlSamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();

            Button button = new Button {
                Text = "Navigate!", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, WidthRequest = 150   
            };

            button.Clicked += async (sender, args) => {
                await Navigation.PushAsync(new XamlPlusCodePage());
            };

            Button btncalculator = new Button {
                Text = "Calculator", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, WidthRequest = 150
            };

            btncalculator.Clicked += async (sender, args) => {
                await DisplayAlert("Alert","Coming Shortly!!!","OK","CANCEL");
            };

            Button btnTime = new Button {
                Text = "Time", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, WidthRequest = 150
            };

            btnTime.Clicked += async (sender, args) => {
                await DisplayAlert("Time", LocalDate(true), "OK", "CANCEL");
            };

            Button btnDate = new Button {
                Text = "Date",HorizontalOptions = LayoutOptions.Center,VerticalOptions = LayoutOptions.Center,WidthRequest = 150
            };

            btnDate.Clicked += async (sender, args) => {
                await DisplayAlert("Time", LocalDate(false), "OK", "CANCEL");
            };

            StackLayout stack = new StackLayout { Children = { button, btncalculator, btnTime, btnDate } };
            Content = stack;

        }

        private string LocalDate(bool time = false)
        {
            string result = string.Empty;
            result = System.DateTime.Now.ToString("g");
            if (time)
                result = System.DateTime.Now.ToString("hh:mm:ss");

            return result;
        }


    }
}
