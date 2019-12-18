
using Xamarin.Forms;

namespace XamlSamples
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Button button = new Button
            {
                Text = "Navigate!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            button.Clicked += async (sender, args) =>
            {
                //await Navigation.PushAsync(new HelloXamlPage());
                await Navigation.PushAsync(new XamlPlusCodePage());
            };

            Content = button;
        }
    }
}
