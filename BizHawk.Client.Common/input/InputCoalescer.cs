using System.Linq;

namespace BizHawk.Client.Common
{
	/// <summary>
	/// coalesces events back into instantaneous states
	/// </summary>
	public class InputCoalescer : SimpleController
	{
		public void Receive(Input.InputEvent ie)
		{
			bool state = ie.EventType == Input.InputEventType.Press;
		
			string button = ie.LogicalButton.ToString();
			Buttons[button] = state;

			//when a button is released, all modified variants of it are released as well
			if (!state)
			{
				var releases = Buttons.Where(kvp => kvp.Key.Contains("+") && kvp.Key.EndsWith(ie.LogicalButton.Button)).ToArray();
				foreach (var kvp in releases)
					Buttons[kvp.Key] = false;
			}
		}
	}
}