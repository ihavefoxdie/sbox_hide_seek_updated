using HideAndSeek.GameLoop.StateMachines;
using HideAndSeek.GameLoop.States;

namespace Sandbox.GameLoop.States;

public class RoundStateResolver
{
	public static RoundState ResolveState( RoundState state, RoundStateMachine stateMachine )
	{
		return state switch
		{
			PreparingRoundState => new ActiveRoundState( stateMachine ),
			ActiveRoundState => new PostRoundState( stateMachine ),
			PostRoundState => new PreparingRoundState( stateMachine ),
			WaitingRoundState => new PreparingRoundState( stateMachine ),
			_ => new WaitingRoundState( stateMachine ),
		};
	}
}
