using Sandbox.UI;

namespace HideAndSeek.UI.MainMenu;

public class GameTitle : Panel
{
	private Label TextLabel;

	public string Text
	{
		get => TextLabel?.Text;
		set
		{
			if ( string.IsNullOrEmpty( value ) )
			{
				TextLabel.Style.Display = DisplayMode.None;
				TextLabel.Text = "";
				return;
			}

			TextLabel.Style.Display = DisplayMode.Flex;
			TextLabel.Text = value;
		}
	}

	public GameTitle()
	{
		TextLabel = new()
		{
			Text = "",
			Parent = this
		};
	}

	public override void Tick()
	{
		base.Tick();
	}
}
