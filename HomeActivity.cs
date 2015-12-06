
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
using Android.Bluetooth;

namespace BluetoothChat
{
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

			SetContentView (Resource.Layout.Home);

			var butt = FindViewById<Button> (Resource.Id.gameButton);
			butt.Click += (object sender, EventArgs e) => {

				if(!adapter.IsEnabled){

					Intent enableIntent = new Intent (BluetoothAdapter.ActionRequestEnable);
					StartActivityForResult (enableIntent, 1);
				}

				AlertDialog.Builder builder = new AlertDialog.Builder(this, 5);
				builder.SetTitle("Start Game?");
				builder.SetMessage("Do you want to start a new game or connect to an exsiting one?");
				builder.SetPositiveButton("New Game", (s, ev) => {

					SetContentView(Resource.Layout.TextInput);

					var number = FindViewById<EditText>(Resource.Id.numPlayers);

					var name = FindViewById<EditText>(Resource.Id.editText1);


					var button = FindViewById<Button>(Resource.Id.createButton);

					button.Click += (object sender1, EventArgs e1) => {

						Console.WriteLine(number.Text.ToString());
						Console.WriteLine(name.Text.ToString());

						adapter.SetName(name.Text.ToString());

						Intent discoverIntent = new Intent(BluetoothAdapter.ActionRequestDiscoverable);
						discoverIntent.PutExtra(BluetoothAdapter.ExtraDiscoverableDuration, 0);
						StartActivity(discoverIntent);

						SetContentView(Resource.Layout.Home);
					};

				});

				builder.SetNegativeButton("Existing Game", (s, ev) => {

					StartActivity(typeof(DeviceListActivity));
				});



				Dialog dialog = builder.Create();
				dialog.Show();
			};

			var db = FindViewById<Button>(Resource.Id.drawTest);
			db.Click+= (object sender2, EventArgs e2) => {

				DrawView dv = new DrawView(this);

				SetContentView(Resource.Layout.DrawingView);
			};
		}
	}
}

