using System;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using System.Collections;

namespace BluetoothChat
{
	/// <summary>
	/// This activity handles all bluetooth aspects
	/// </summary>
	public class BlueHandle
	{
		//Variables
		public int maxDevices;
		private ArrayList messages = new ArrayList ();
		private int devices = 0;
		private int directDevices = 0;

		// Debugging
		private const string TAG = "BluetoothChat";
		private const bool Debug = true;

		// Message types sent from the BluetoothChatService Handler
		// TODO: Make into Enums
		public const int MESSAGE_STATE_CHANGE = 1;
		public const int MESSAGE_READ = 2;
		public const int MESSAGE_WRITE = 3;
		public const int MESSAGE_DEVICE_NAME = 4;
		public const int MESSAGE_TOAST = 5;

		// Key names received from the BluetoothChatService Handler
		public const string DEVICE_NAME = "device_name";
		public const string INDEX = "index";
		public const string TOAST = "toast";

		// Intent request codes
		private const int REQUEST_CONNECT_DEVICE = 1;

		// Names of connected devices
		protected ArrayList DeviceNames = new ArrayList();

		// Bluetooth Adapter
		private BluetoothAdapter bluetoothAdapter = null;
		// bluetooth service
		private BluetoothChatService service = null;


		//Initialization
		public BlueHandle (BluetoothAdapter adapter, int maxDevices)
		{
			// Get local Bluetooth adapter
			bluetoothAdapter = adapter;
			this.maxDevices = maxDevices;

			// Initialize the BluetoothChatService to perform bluetooth connections
			service = new BluetoothChatService (this, new MyHandler (this));

		}

		//Functions

		public int getNumDevices(){
			return directDevices;
		}
		public int getTotalDevices(){
			return devices;
		}
		/// <summary>
		/// Determines whether this instance has messages the specified readBuf.
		/// </summary>
		/// <returns><c>true</c> if this instance has messages the specified readBuf; otherwise, <c>false</c>.</returns>
		/// <param name="readBuf">Read buffer.</param>
		private bool HasMessages(byte[] readBuf){
			foreach (byte[] temp in messages) {
				if (temp.Equals(readBuf)){
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Devices the found.
		/// </summary>
		/// <returns><c>true</c>, if device found, <c>false</c> otherwise.</returns>
		/// <param name="devices">Devices.</param>
		private bool DeviceFound (string devices){
			foreach(string device in DeviceNames){
				if(devices.Equals(device)){
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// Raises the destroy event.
		/// </summary>
		protected override void OnDestroy(){
			for (int index = 0; index < BluetoothChatService.SIZE; index++) {
				if (service != null)
					service.Stop (index);
			}
		}

		// The Handler that gets information back from the BluetoothChatService
		private class MyHandler : Handler
		{
			BlueHandle bluetooth;

			public MyHandler (BlueHandle blue)
			{
				bluetooth = blue;	
			}

			public override void HandleMessage (Message msg)
			{
				switch (msg.What) {
				case MESSAGE_STATE_CHANGE:
					if (Debug)
						Log.Info (TAG, "MESSAGE_STATE_CHANGE: " + msg.Arg1);
					switch (msg.Arg1) {
					case BluetoothChatService.STATE_CONNECTED:
						//bluetoothChat.title.SetText (Resource.String.title_connected_to);
						//bluetoothChat.title.Append (bluetoothChat.connectedDeviceName);
						//bluetoothChat.conversationArrayAdapter.Clear ();
						break;
					case BluetoothChatService.STATE_CONNECTING:
						break;
					case BluetoothChatService.STATE_LISTEN:
					case BluetoothChatService.STATE_NONE:
						//TODO what if someone disconnects
						// which one disconnected?
						// .Remove();
						//bluetoothChat.title.SetText (Resource.String.title_not_connected);
						break;
					}
					break;
				case MESSAGE_WRITE:
					break;
				case MESSAGE_READ:
					byte[] readBuf = (byte[])msg.Obj;
					if(!HasMessages(readBuf)){
						messages.Add (readBuf);
						// TODO DECODE and determine if is a device list
						// if it is a device list, add new devices to the count
						// TODO read to method
						// blank.RecieveMessage(readBuf);
						// forward the message
						SendMessage(readBuf);
					}
					break;
					// saves the device to the list of devices
				case MESSAGE_DEVICE_NAME:
					if (!DeviceFound (msg.Data.GetString (DEVICE_NAME))) {
						bluetooth.DeviceNames.Add (msg.Data.GetString (DEVICE_NAME));
						devices++;
						directDevices++;
					}
					//Toast.MakeText (Application.Context, "Connected to " + bluetoothChat.connectedDeviceName, ToastLength.Short).Show ();
					break;
				case MESSAGE_TOAST:
					Toast.MakeText (Application.Context, msg.Data.GetString (TOAST), ToastLength.Short).Show ();
					break;					
				}
			}
		}

		/// <summary>
		/// Raises the activity result event.
		/// Connects to a new device from deviceListActivity
		/// </summary>
		/// <param name="requestCode">Request code.</param>
		/// <param name="resultCode">Result code.</param>
		/// <param name="data">Data.</param>
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (Debug)
				Log.Debug (TAG, "onActivityResult " + resultCode);

			switch(requestCode)
			{
			case REQUEST_CONNECT_DEVICE:
				// When DeviceListActivity returns with a device to connect
				if( resultCode == Result.Ok)
				{
					// Get the device MAC address
					var address = data.Extras.GetString(DeviceListActivity.EXTRA_DEVICE_ADDRESS);
					// Get the BLuetoothDevice object
					BluetoothDevice device = bluetoothAdapter.GetRemoteDevice(address);
					// Attempt to connect to the device
					service.Connect(device);
				}
				break;
			}
		}

		/// <summary>
		/// Sends a message.
		/// </summary>
		/// <param name='message'>
		/// A array of bytes to send.
		/// </param>
		private void SendMessage (byte[] message)
		{
			// Get the message bytes and tell the BluetoothChatService to write
			// Floods the message to all devices
			for (int index = 0; index < BluetoothChatService.SIZE; index++) {
				service.Write (message, index);
			}

		}
	}
}

