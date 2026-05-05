using HideAndSeek.GameLoop.Rules;

namespace HideAndSeek.GameLoop;

public sealed class GameManager : Component
{
	protected override void OnAwake()
	{
		GameObject.AddComponent<GameLoop>();
	}

	protected override void OnUpdate()
	{

	}

	protected override void OnDisabled()
	{
		base.OnDisabled();
	}
}
