using System;

namespace BluetoothChat
{
	public struct MessageStruct
	{
		short destination;
		short source;
		bool pass;
		bool type;
		int length;
		byte[] data;

		public int Length {
			get {
				return length;
			}
			set{
				length = value;
			}
		}

		public bool Pass {
			get {
				return pass;
			}
			set{ 
				pass = value;
			}
		}

		public byte[] Data {
			get {
				return data;
			}
			set{ data = value;}
		}

		public bool Type {
			get {
				return type;
			}
			set {
				data = value;
			}
				
		}
	}
}

