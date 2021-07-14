using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloXamarin.Droid
{
    [BroadcastReceiver(Enabled =true)]
    [IntentFilter(new[] {"com.honeywell.decode.intent.action.EDIT_DATA" })]
    public class Receiver1 : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            intent.GetStringExtra("key");
            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();
        }


    }
}