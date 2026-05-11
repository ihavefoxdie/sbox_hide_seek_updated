using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.StateMachines;
using System;

namespace HideAndSeek.GameLoop;

public sealed class GameLoop : Component, Component.INetworkListener
{
	private UserGameSettings _gameSettings;
	private RoundStateMachine _roundStateMachine;

	[Sync( SyncFlags.FromHost )]
	public Team Seekers { get; set; }
	[Sync( SyncFlags.FromHost )]
	public Team Hiders { get; set; }

	[Property]
	[Sync( SyncFlags.FromHost )]
	public Round Round { get; set; }

	protected override void OnAwake()
	{
		_gameSettings = new UserGameSettings();

		if ( Networking.IsHost )
		{
			Hiders = GameObject.AddComponent<Team>();
			Seekers = GameObject.AddComponent<Team>();
			Round = GameObject.AddComponent<Round>();

			Round.Init( 1, 1, 1, 1 );
			Seekers.Init( "Seekers", "Red" );
			Hiders.Init( "Hiders", "Blue" );
		}
			
		_roundStateMachine = GameObject.GetOrAddComponent<RoundStateMachine>();
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

	public void OnDisconnected( Connection connection )
	{
		Hiders.RemovePlayer( connection.Id );
		Seekers.RemovePlayer( connection.Id );
	}

	public void OnActive( Connection connection )
	{
	}

	public void AssignPlayers()
	{
		if ( !Networking.IsHost )
		{
			Log.Info( "Only the host can assign players to teams." );
			return;
		}
		Log.Info( "Assigning players to teams." );

		int seekersCount = Math.Max( Connection.All.Count / 10, 1 );

		for ( int i = 0; i < seekersCount; i++ )
		{
			int index = Game.Random.Next( Connection.All.Count );
			var connection = Connection.All.ElementAt( index );

			Seekers.AddPlayer( connection.Id );
			//RespawnPlayer( connId );
		}

		//assigning the remaining players to hiders team
		for ( int i = 0; i < Connection.All.Count; i++ )
		{
			if ( !Seekers.IsThePlayerInTeam( Connection.All[i].Id ) )
			{
				//RespawnPlayer( Connection.All[i].Id, "hiders" );
				Hiders.AddPlayer( Connection.All[i].Id );
			}
		}
	}
}
