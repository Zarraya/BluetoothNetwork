
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using Android.Graphics;

namespace BluetoothChat
{
	[Register("BluetoothChat.DrawingView")]
	public class DrawView : View
	{

		private Path drawPath;
		private Paint drawPaint, canvasPaint;
		private Canvas drawCanvas;
		private Bitmap canvasBitmap;

		public DrawView (Context context) :
			base (context)
		{
			Initialize ();
		}

		public DrawView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public DrawView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{

			drawPath = new Path ();
			drawPaint = new Paint ();

			drawPaint.AntiAlias = true;
			drawPaint.StrokeWidth = 15;
			drawPaint.SetStyle (Paint.Style.Stroke);
			drawPaint.StrokeJoin = Paint.Join.Round;
			drawPaint.StrokeCap = Paint.Cap.Round;

			canvasPaint = new Paint (PaintFlags.Dither);



			var drawButt = FindViewById<Button> (Resource.Id.drawButton);
			drawButt.Click += (object sender, EventArgs e) => {

				setColor(false);
			};

			var clearButt = FindViewById<Button> (Resource.Id.clearButton);
			clearButt.Click += (object sender, EventArgs e) => {

				setColor(true);
			};
		}

		public void setColor(bool erase){

			Invalidate ();

			if (!erase) {

				drawPaint.Color = Color.Black;
			} else {
				drawPaint.Color = Color.White;
			}
		}
	}
}

