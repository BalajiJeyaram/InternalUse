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
	public partial class XamlPlusCodePage : ContentPage
	{
        //private Label valueLabel;

        //private void InitialComponenent()
        //{
        //    this.LoadFromXaml(typeof(XamlPlusCodePage));
        //    valueLabel = this.FindByName<Label>("valueLabel");
        //}
		public XamlPlusCodePage ()
		{
			InitializeComponent ();
		}

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            //valueLabel.Text = e.NewValue.ToString("F3");
            valueLabel.Text = ((Slider)sender).Value.ToString("F3");
        }

        async void OnButtonClicked(Object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text +" ' has been changed",
                "OK");

        }
    }
}