
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
		public static HomeActivity home;

		public const int MESSAGE_STATE_CHANGE = 1;
		public const int MESSAGE_READ = 2;
		public const int MESSAGE_WRITE = 3;
		public const int MESSAGE_DEVICE_NAME = 4;
		public const int MESSAGE_TOAST = 5;

		BluetoothChatService chatService;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			home = this;

			BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;

			chatService = new BluetoothChatService (this, new MyHandler(new Message() message));

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

		public void initiateConnection(string address){

			BluetoothDevice device = BluetoothAdapter.DefaultAdapter.GetRemoteDevice (address);

			chatService.Connect (device);
		}
	}

	// The Handler that gets information back from the BluetoothChatService
	class MyHandler : Handler
	{

		public const int MESSAGE_STATE_CHANGE = 1;
		public const int MESSAGE_READ = 2;
		public const int MESSAGE_WRITE = 3;
		public const int MESSAGE_DEVICE_NAME = 4;
		public const int MESSAGE_TOAST = 5;

		BluetoothChat bluetoothChat;

		public MyHandler (BluetoothChat chat)
		{
			bluetoothChat = chat;	
		}

		public override void HandleMessage (Message msg)
		{
			switch (msg.What) {
			case MESSAGE_STATE_CHANGE:
				//if (Debug)
					//Log.Info (TAG, "MESSAGE_STATE_CHANGE: " + msg.Arg1);
				switch (msg.Arg1) {
				case BluetoothChatService.STATE_CONNECTED:
					//bluetoothChat.title.SetText (Resource.String.title_connected_to);
					//bluetoothChat.title.Append (bluetoothChat.connectedDeviceName);
					//bluetoothChat.conversationArrayAdapter.Clear ();
					break;
				case BluetoothChatService.STATE_CONNECTING:
					//bluetoothChat.title.SetText (Resource.String.title_connecting);
					break;
				case BluetoothChatService.STATE_LISTEN:
				case BluetoothChatService.STATE_NONE:
					//bluetoothChat.title.SetText (Resource.String.title_not_connected);
					break;
				}
				break;
			case MESSAGE_WRITE:
				byte[] writeBuf = (byte[])msg.Obj;
				// construct a string from the buffer
				var writeMessage = new Java.Lang.String (writeBuf);
				//bluetoothChat.conversationArrayAdapter.Add ("Me: " + writeMessage);
				break;
			case MESSAGE_READ:
				byte[] readBuf = (byte[])msg.Obj;
				// construct a string from the valid bytes in the buffer
				var readMessage = new Java.Lang.String (readBuf, 0, msg.Arg1);
				//bluetoothChat.conversationArrayAdapter.Add (bluetoothChat.connectedDeviceName + ":  " + readMessage);
				break;
			case MESSAGE_DEVICE_NAME:
				// save the connected device's name
				//bluetoothChat.connectedDeviceName = msg.Data.GetString (DEVICE_NAME);
				//Toast.MakeText (Application.Context, "Connected to " + bluetoothChat.connectedDeviceName, ToastLength.Short).Show ();
				break;
			case MESSAGE_TOAST:
				//Toast.MakeText (Application.Context, msg.Data.GetString (TOAST), ToastLength.Short).Show ();
				break;
			}
		}
	}
}

