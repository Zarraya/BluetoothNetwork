
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
	[Activity (Label = "HomeActivity")]			
	public class HomeActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Home);

			var butt = FindViewById<Button> (Resource.Id.gameButton);
			butt.Click += (object sender, EventArgs e) => {

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

						Finish();
					};

				});

				builder.SetNegativeButton("Existing Game", (s, ev) => {


				});

				Dialog dialog = builder.Create();
				dialog.Show();
			};
		}
	}
}

