using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.States;
using Sandbox.GameLoop.States;
using System;

namespace HideAndSeek.GameLoop.StateMachines
{
	public class RoundStateMachine : Component
	{
		public GameLoop GameLoop;

		private RoundState _currentState;
		public event Action<IState> OnStateChanged;

		[Rpc.Broadcast]
		public void ChangeState( RoundState state )
		{
			_currentState?.Exit();
			_currentState = RoundStateResolver.ResolveState( state, this );
			_currentState.Enter();
			OnStateChanged?.Invoke( _currentState );
		}

		protected override void OnAwake()
		{
			GameLoop = Scene.GetAllComponents<GameLoop>().FirstOrDefault();
			if ( Networking.IsHost )
			{
				ChangeState( null );
			}
		}

		protected override void OnUpdate()
		{
			if ( Networking.IsHost )
				_currentState?.Update();
		}
	}
}
