using System;

namespace BluetoothChat
{
	public struct MessageStruct
	{
		short destination;
		short source;
		bool pass;
		int length;
		byte[] data;

	}
}

