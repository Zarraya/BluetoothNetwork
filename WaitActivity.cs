
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BluetoothChat
{
	[Activity (Label = "WaitActivity", Theme = "@style/Theme.Main")]			
	public class WaitActivity : Activity
	{

		private int totalDevices = 0;
		private int connectedDevices = 0;
		protected TextView text;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.WaitView);
		}

		public void update(int total, int connected){

			totalDevices = total;
			connectedDevices = connected;

			text = FindViewById<TextView> (Resource.Id.textView);
			text.Text = connectedDevices + " / " + totalDevices + " Devices Connected";
		}
	}
}

