using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.StateMachines;
using System;

namespace HideAndSeek.GameLoop;

public sealed class GameLoop : Component, Component.INetworkSnapshot
{
	private UserGameSettings _gameSettings;
	private RoundStateMachine _roundStateMachine;

	[Property]
	[Sync( SyncFlags.FromHost )]
	public Team[] Teams { get; set; }

	[Property]
	[Sync( SyncFlags.FromHost )]
	public Round Round { get; set; }

	protected override void OnAwake()
	{
		_gameSettings = new UserGameSettings();
		if ( Networking.IsHost )
		{
			Round = new Round();
			Teams =
			[
				new Team("Seekers", "Red"),
				new Team("Hiders", "Blue")
			];
		}
			
		_roundStateMachine= GameObject.GetOrAddComponent<RoundStateMachine>();
	}

	protected override void OnEnabled()
	{
	}

	protected override void OnFixedUpdate()
	{
		if ( Networking.IsHost )
		{
		}
	}


	public void AssignPlayers()
	{
		int seekersCount = Math.Max( Connection.All.Count / 10, 1 );
		Team seekers = Teams.FirstOrDefault( x => x.Name == "Seekers" );
		Team hiders = Teams.FirstOrDefault( x => x.Name == "Hiders" );

		for ( int i = 0; i < seekersCount; i++ )
		{
			int index = Game.Random.Next( Connection.All.Count );
			var connection = Connection.All.ElementAt( index );

			if ( seekers.IsThePlayerInTeam( connection.Id ) )
				continue;

			seekers.AddPlayer( connection.Id );
			//RespawnPlayer( connId );
		}

		//assigning the remaining players to hiders team
		for ( int i = 0; i < Connection.All.Count; i++ )
		{
			if ( !seekers.IsThePlayerInTeam( Connection.All[i].Id ) )
			{
				// Spawn this object and make the client the owner
				//RespawnPlayer( Connection.All[i].Id, "hiders" );
				hiders.AddPlayer( Connection.All[i].Id );
			}
		}
	}
}
