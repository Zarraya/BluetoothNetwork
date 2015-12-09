
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
	

	[Activity (Label = "HomeActivity", Theme = "@style/Theme.Main")]			
	public class HomeActivity : Activity
	{
		protected BlueHandle handle;
		protected bool activeReturn = false;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			handle = new BlueHandle ();

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

					activeReturn = true;

					Intent serverIntent = new Intent(this, typeof(DeviceListActivity));

					StartActivityForResult(serverIntent, 1);
				});



				Dialog dialog = builder.Create();
				dialog.Show();
			};

			var db = FindViewById<Button>(Resource.Id.drawTest);
			db.Click+= (object sender2, EventArgs e2) => {

				Android.Graphics.Bitmap image;

				LinearLayout layout = new LinearLayout (this);
				layout.Orientation = Orientation.Vertical;
				layout.SetBackgroundColor(Android.Graphics.Color.White);
				LinearLayout horiLayout = new LinearLayout (this);
				horiLayout.SetBackgroundColor(Android.Graphics.Color.SlateGray);
				horiLayout.Orientation = Orientation.Horizontal;

				Button drawButt = new Button (this);
				drawButt.Text = "Draw";
				horiLayout.AddView(drawButt);

				Button eraseButt = new Button (this);
				eraseButt.Text = "Erase";
				horiLayout.AddView(eraseButt);

				Button clearButt = new Button (this);
				clearButt.Text = "Clear";
				horiLayout.AddView(clearButt);

				Button doneButt = new Button (this);
				doneButt.Text = "Done";
				horiLayout.AddView(doneButt);

				DrawTest dt = new DrawTest(this);

				layout.AddView(horiLayout);
				layout.AddView(dt);

				drawButt.Click += (object sender, EventArgs e) => {

					dt.setColor(false);
				};

				eraseButt.Click += (object sender, EventArgs e) => {

					dt.setColor(true);
				};

				clearButt.Click += (object sender, EventArgs e) => {

					dt.clear();
				};

				doneButt.Click += (object sender, EventArgs e) => {

					image = dt.done();
					SetContentView(Resource.Layout.Home);
				};

				SetContentView(layout);
				//SetContentView(new DrawTest(this));

				//DrawView dv = new DrawView(this);
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (activeReturn) {
				base.OnActivityResult (requestCode, resultCode, data);

				//handle.giveResult (requestCode, resultCode, data);

				activeReturn = false;
			}
		}
			
	}



}

