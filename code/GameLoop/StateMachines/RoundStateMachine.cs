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
			_currentState = RoundStateResolver.ResolveState( state, this );
		}

		protected override void OnAwake()
		{
			GameLoop = Scene.GetAllComponents<GameLoop>().FirstOrDefault();
		}

		protected override void OnUpdate()
		{
			if ( _currentState != null )
			{
				_currentState.Update();
			}
		}
	}
}
