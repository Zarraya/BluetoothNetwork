package md52cab87e79568059b98e61cae51903d9d;


public class BlueHandle_MyHandler
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_handleMessage:(Landroid/os/Message;)V:GetHandleMessage_Landroid_os_Message_Handler\n" +
			"";
		mono.android.Runtime.register ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BlueHandle_MyHandler.class, __md_methods);
	}


	public BlueHandle_MyHandler () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BlueHandle_MyHandler.class)
			mono.android.TypeManager.Activate ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public BlueHandle_MyHandler (android.os.Handler.Callback p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == BlueHandle_MyHandler.class)
			mono.android.TypeManager.Activate ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public BlueHandle_MyHandler (android.os.Looper p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == BlueHandle_MyHandler.class)
			mono.android.TypeManager.Activate ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public BlueHandle_MyHandler (android.os.Looper p0, android.os.Handler.Callback p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == BlueHandle_MyHandler.class)
			mono.android.TypeManager.Activate ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

	public BlueHandle_MyHandler (md52cab87e79568059b98e61cae51903d9d.BlueHandle p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BlueHandle_MyHandler.class)
			mono.android.TypeManager.Activate ("BluetoothChat.BlueHandle+MyHandler, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "BluetoothChat.BlueHandle, BluetoothChat, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void handleMessage (android.os.Message p0)
	{
		n_handleMessage (p0);
	}

	private native void n_handleMessage (android.os.Message p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}