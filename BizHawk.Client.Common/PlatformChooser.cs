using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using BizHawk.Emulation.Common;

namespace BizHawk.Client.Common
{
	public partial class PlatformChooser : Form
	{
		public RomGame RomGame { get; set; }
		public string PlatformChoice { get; set; }

		private RadioButton SelectedRadio => PlatformsGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(x => x.Checked);

		public PlatformChooser()
		{
			InitializeComponent();
			AvailableSystems = new SystemLookup().AllSystems.ToList();
		}

		private readonly List<SystemLookup.SystemInfo> AvailableSystems;

		private void PlatformChooser_Load(object sender, EventArgs e)
		{
			RomSizeLabel.Text = RomGame.RomData.Length > 0xA00000 // 10 MiB
				? $"{RomGame.RomData.Length << 20:N0}MiB"
				: $"{RomGame.RomData.Length << 10:N0}KiB";
			ExtensionLabel.Text = RomGame.Extension.ToLower();
			HashBox.Text = RomGame.GameInfo.Hash;
			int count = 0;
			int spacing = 25;
			foreach (var platform in AvailableSystems)
			{
				var radio = new RadioButton
				{
					Text = platform.FullName,
					Location = UIHelper.Scale(new Point(15, 15 + (count * spacing))),
					Size = UIHelper.Scale(new Size(200, 23))
				};

				PlatformsGroupBox.Controls.Add(radio);
				count++;
			}

			PlatformsGroupBox.Controls
				.OfType<RadioButton>()
				.First()
				.Select();
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OkBtn_Click(object sender, EventArgs e)
		{
			var selectedValue = SelectedRadio != null ? SelectedRadio.Text : "";
			PlatformChoice = AvailableSystems.FirstOrDefault(x => x.FullName == selectedValue).SystemId;

			if (AlwaysCheckbox.Checked)
			{
				Global.Config.PreferredPlatformsForExtensions[RomGame.Extension.ToLower()] = PlatformChoice;
			}

			Close();
		}

		private void label4_Click(object sender, EventArgs e)
		{
			AlwaysCheckbox.Checked ^= true;
		}
	}
}
