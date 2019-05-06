using System;
using System.Windows.Forms;

namespace BizHawk.Client.Common
{
	/// <summary>
	/// This class adds on to the functionality provided in System.Windows.Forms.MenuStrip.
	/// </summary>
	public class MenuStripEx : MenuStrip
	{
		private const uint MA_ACTIVATE = 0x1;
		private const uint MA_ACTIVATEANDEAT = 0x2;
		private const uint WM_MOUSEACTIVATE = 0x21;

		private bool clickThrough = true;

		/// <summary>
		/// Gets or sets whether the ToolStripEx honors item clicks when its containing form does
		/// not have input focus.
		/// </summary>
		public bool ClickThrough
		{
			get { return clickThrough; }
			set { clickThrough = value; }
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (clickThrough &&
				m.Msg == WM_MOUSEACTIVATE &&
				m.Result == (IntPtr)MA_ACTIVATEANDEAT)
			{
				m.Result = (IntPtr)MA_ACTIVATE;
			}
		}
	}
}