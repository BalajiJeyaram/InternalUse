using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamlSamples
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
		public CalculatorPage ()
		{
			InitializeComponent ();
            Content = InitialButtons();
		}

        StackLayout  InitialButtons()
        {
           
            Button btnone = new Button
            {
                Text = "1-Abc",
                HorizontalOptions = LayoutOptions.Start,
                WidthRequest = 50
            };

            btnone.Clicked += async (sender, args) => {
                await DisplayAlert("Alert", "You have clicked One", "OK","Cancel");
            };

            Button btntwo = new Button
            {
                Text = "2-def",
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 50
            };

            btntwo.Clicked += async (sender, args) => {
                await DisplayAlert("Alert", "You have clicked Two", "OK", "Cancel");
            };

            Button btnthree = new Button
            {
                Text = "3-ghi",
                HorizontalOptions = LayoutOptions.End,
                WidthRequest = 50
            };

            btnthree.Clicked += async (sender, args) => {
                await DisplayAlert("Alert", "You have clicked Three", "OK", "Cancel");
            };

            StackLayout stack = new StackLayout { Children = { btnone, btntwo, btnthree}, Orientation = StackOrientation.Horizontal};
            return stack;
        }
	}
}