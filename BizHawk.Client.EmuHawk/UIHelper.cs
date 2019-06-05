using System;
using System.Drawing;
using System.Windows.Forms;

using JetBrains.Annotations;

namespace BizHawk.Client.Common
{
	public static class UIHelper
	{
		[NotNull] private static readonly AutoScaleMode _autoScaleMode = AutoScaleMode.Font;
		[NotNull] private static readonly SizeF _autoScaleBaseSize = new SizeF(6F, 13F);
		[NotNull] private static readonly SizeF _autoScaleCurrentSize = GetCurrentAutoScaleSize(_autoScaleMode);

		private static SizeF GetCurrentAutoScaleSize(AutoScaleMode autoScaleMode)
		{
			using (Form form = new Form())
			{
				form.AutoScaleMode = autoScaleMode;
				return form.CurrentAutoScaleDimensions;
			}
		}

		public static AutoScaleMode AutoScaleMode
		{
			get { return _autoScaleMode; }
		}

		public static SizeF AutoScaleBaseSize
		{
			get { return _autoScaleBaseSize; }
		}

		public static float AutoScaleFactorX
		{
			get { return _autoScaleCurrentSize.Width / _autoScaleBaseSize.Width; }
		}

		public static float AutoScaleFactorY
		{
			get { return _autoScaleCurrentSize.Height / _autoScaleBaseSize.Height; }
		}

		public static int ScaleX(int size)
		{
			return (int)Math.Round(size * AutoScaleFactorX);
		}

		public static int ScaleY(int size)
		{
			return (int)Math.Round(size * AutoScaleFactorY);
		}

		public static Point Scale(Point p)
		{
			return new Point(ScaleX(p.X), ScaleY(p.Y));
		}

		public static Size Scale(Size s)
		{
			return new Size(ScaleX(s.Width), ScaleY(s.Height));
		}
	}
}
