using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using BizHawk.Common;
using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	public abstract class ClientMainFormCommon : Form
	{
		#region static fields and properties

		protected static readonly Func<RomGame, string> ChoosePlatformForRom = rom =>
		{
			var platformChooser = new PlatformChooser { RomGame = rom };
			platformChooser.ShowDialog();
			return platformChooser.PlatformChoice;
		};

		#endregion

		#region fields and properties

		protected AutofireController AutofireNullControls;

//		/// <summary>
//		/// input state which has been destined for game controller inputs are coalesced here
//		/// </summary>
//		protected readonly ControllerInputCoalescer ControllerCoalescer = new ControllerInputCoalescer();

		/// <summary>
		/// input state which has been destined for client hotkey consumption are coalesced here
		/// </summary>
		protected readonly InputCoalescer HotkeyCoalescer = new InputCoalescer();

		/// <summary>
		/// This is a quick hack to reduce the dependency on Global.Emulator
		/// </summary>
		/// <remarks>
		/// TODO: make this an actual property, set it when loading a Rom, and pass it dialogs, etc
		/// </remarks>
		protected virtual IEmulator Emulator
		{
			get { return Global.Emulator; }
			set { Global.Emulator = value; }
		}

		public virtual bool IsTurboing => Global.ClientControls["Turbo"];

		#endregion

		#region abstract properties

		protected abstract MenuStripEx FormMenuTopLevel { get; }

		#endregion

		#region static methods

		protected static void CheckMessages()
		{
			Application.DoEvents();
			if (ActiveForm != null) ScreenSaver.ResetTimerPeriodically();
		}

		protected static void CoreSettings(object sender, RomLoader.SettingsLoadArgs e)
		{
			e.Settings = Global.Config.GetCoreSettings(e.Core);
		}

		/// <param name="args">flattened Tuple&lt;string, string>[]</param>
		/// <remarks>
		/// TODO use actual tuples
		/// </remarks>
		public static string FormatFilter(params string[] args)
		{
			if (args.Length % 2 == 1) throw new ArgumentException();
			var list = new List<string>();
			for (var i = 0; i < args.Length; i += 2)
				list.Add(string.Format("{0} ({1})|{1}", args[i], args[i + 1]));
			return string.Join("|", list)
				.Replace("%ARCH%", "*.zip;*.rar;*.7z;*.gz") //TODO use %ARCHIVE% or %RCHV%, %ARCH% could be misinterpreted as CPU architecture
				.Replace(";", "; ");
		}

		protected static bool StateErrorAskUser(string title, string message) =>
			MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

		#endregion
		
		#region methods

		protected void InitControls()
		{
			var controls = new Controller(
				new ControllerDefinition
				{
					Name = "Emulator Frontend Controls",
					BoolButtons = Global.Config.HotkeyBindings.Select(x => x.DisplayName).ToList()
				});

			foreach (var b in Global.Config.HotkeyBindings) controls.BindMulti(b.DisplayName, b.Bindings);

			Global.ClientControls = controls;
			AutofireNullControls = new AutofireController(NullController.Instance.Definition, Emulator);
		}

		protected int? LoadArhiveChooser(HawkFile file)
		{
			var ac = new ArchiveChooser(file);
			return ac.ShowDialog(this) == DialogResult.OK ? ac.SelectedMemberIndex : (int?) null; //TODO use default on move to C# 7.0
		}

		protected virtual void ProcessInput()
		{
			// loop through all available events
			Input.InputEvent ie;
			while ((ie = Input.Instance.DequeueEvent()) != null)
			{
				// TODO - wonder what happens if we pop up something interactive as a response to one of these hotkeys? may need to purge further processing

				// look for hotkey bindings for this key
				var triggers = Global.ClientControls.SearchBindings(ie.LogicalButton.ToString());
				if (triggers.Count == 0)
				{
					// Maybe it is a system alt-key which hasnt been overridden
					if (ie.EventType == Input.InputEventType.Press)
					{
						if (ie.LogicalButton.Alt && ie.LogicalButton.Button.Length == 1)
						{
							var c = ie.LogicalButton.Button.ToLower()[0];
							if (('a' <= c && c <= 'z') || c == ' ')
							{
								SendAltKeyChar(c);
							}
						}

						if (ie.LogicalButton.Alt && ie.LogicalButton.Button == "Space")
						{
							SendPlainAltKey(32);
						}
					}

					// ordinarily, an alt release with nothing else would move focus to the menubar. but that is sort of useless, and hard to implement exactly right.
				}

				// zero 09-sep-2012 - all input is eligible for controller input. not sure why the above was done.
				// maybe because it doesnt make sense to me to bind hotkeys and controller inputs to the same keystrokes

				// adelikat 02-dec-2012 - implemented options for how to handle controller vs hotkey conflicts. This is primarily motivated by computer emulation and thus controller being nearly the entire keyboard
				bool handled;
				var conInput = (ControllerInputCoalescer) Global.ControllerInputCoalescer;
				switch (Global.Config.Input_Hotkey_OverrideOptions)
				{
					default:
					case 0: // Both allowed
						conInput.Receive(ie);

						handled = false;
						if (ie.EventType == Input.InputEventType.Press)
						{
							handled = triggers.Aggregate(false, (current, trigger) => current | CheckHotkey(trigger));
						}

						// hotkeys which arent handled as actions get coalesced as pollable virtual client buttons
						if (!handled)
						{
							HotkeyCoalescer.Receive(ie);
						}

						break;
					case 1: // Input overrides Hokeys
						conInput.Receive(ie);
						if (!Global.ActiveController.HasBinding(ie.LogicalButton.ToString()))
						{
							handled = false;
							if (ie.EventType == Input.InputEventType.Press)
							{
								handled = triggers.Aggregate(false, (current, trigger) => current | CheckHotkey(trigger));
							}

							// hotkeys which arent handled as actions get coalesced as pollable virtual client buttons
							if (!handled)
							{
								HotkeyCoalescer.Receive(ie);
							}
						}

						break;
					case 2: // Hotkeys override Input
						handled = false;
						if (ie.EventType == Input.InputEventType.Press)
						{
							handled = triggers.Aggregate(false, (current, trigger) => current | CheckHotkey(trigger));
						}

						// hotkeys which arent handled as actions get coalesced as pollable virtual client buttons
						if (!handled)
						{
							HotkeyCoalescer.Receive(ie);

							// Check for hotkeys that may not be handled through CheckHotkey method, reject controller input mapped to these
							if (!triggers.Any(IsInternalHotkey))
							{
								conInput.Receive(ie);
							}
						}

						break;
				}
			}
		}

		/// <summary>
		/// sends an alt+mnemonic combination
		/// </summary>
		private void SendAltKeyChar(char c)
		{
			switch (PlatformLinkedLibSingleton.CurrentOS)
			{
				case PlatformLinkedLibSingleton.DistinctOS.Linux:
				case PlatformLinkedLibSingleton.DistinctOS.macOS:
					// no mnemonics for you
					break;
				case PlatformLinkedLibSingleton.DistinctOS.Windows:
					//HACK
					var _ = typeof(ToolStrip).InvokeMember(
						"ProcessMnemonicInternal",
						BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance,
						null,
						FormMenuTopLevel,
						new object[] { c });
					break;
			}
		}

		/// <summary>
		/// sends a simulation of a plain alt key keystroke
		/// </summary>
		private void SendPlainAltKey(int lparam)
		{
			var m = new Message
			{
				HWnd = Handle,
				LParam = new IntPtr(lparam),
				Msg = 0x0112,
				WParam = new IntPtr(0xF100)
			};
			base.WndProc(ref m);
		}

		protected void ShowMessageCoreComm(string message)
		{
			MessageBox.Show(this, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
		
		#endregion

		#region abstract methods

		/// <summary>
		/// Controls whether the app generates input events. should be turned off for most modal dialogs
		/// </summary>
		public abstract bool AllowInput(bool yieldAlt);

		protected abstract bool CheckHotkey(string trigger);

		/// <summary>
		/// Determines if the value is a hotkey that would be handled outside of the CheckHotkey method
		/// </summary>
		protected abstract bool IsInternalHotkey(string trigger);
		
		#endregion
	}
}