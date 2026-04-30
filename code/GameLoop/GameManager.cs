using HideAndSeek.GameLoop.Rules;

namespace HideAndSeek.GameLoop;

public sealed class GameManager : Component
{
	private Round Round = new();
	private Team[] Teams = new Team[2]
	{
		new Team("Seekers", "Red"),
		new Team("Hiders", "Blue")
	};

	protected override void OnUpdate()
	{

	}
}
