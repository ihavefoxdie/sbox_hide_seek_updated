using HideAndSeek.GameLoop.Rules;
using HideAndSeek.GameLoop.StateMachines;
using System;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public sealed class GameLoop : Component, Component.INetworkListener
{
	private UserGameSettings _gameSettings;
	private RoundStateMachine _roundStateMachine;
	private NetworkComponent _networkComponent;

	[Sync( SyncFlags.FromHost )]
	public Team Seekers { get; set; }
	[Sync( SyncFlags.FromHost )]
	public Team Hiders { get; set; }

	[Property]
	[Sync( SyncFlags.FromHost )]
	public Round Round { get; set; }

	protected override async Task OnLoad()
	{
	}

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

			_networkComponent = GameObject.GetOrAddComponent<NetworkComponent>();
			_networkComponent.PlayerPrefab = GameObject.GetPrefab( "prefabs/player.prefab" );
		}

		_roundStateMachine = GameObject.GetOrAddComponent<RoundStateMachine>();
	}

	protected override void OnStart()
	{
		if ( Networking.IsHost )
		{
			_networkComponent.StartLobby();
		}
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
		if ( Round.IsStarted )
		{
			//Seekers.AddPlayer( connection.Id );
		}
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
			RespawnPlayer( connection );
		}

		//assigning the remaining players to hiders team
		for ( int i = 0; i < Connection.All.Count; i++ )
		{
			if ( !Seekers.IsThePlayerInTeam( Connection.All[i].Id ) )
			{
				RespawnPlayer( Connection.All[i], "hiders" );
				Hiders.AddPlayer( Connection.All[i].Id );
			}
		}
	}


	/// <summary>
	/// Respawn player by providing them a new pawn to play as.
	/// </summary>
	/// <param name="connection">Player connection.</param>
	/// <param name="tag">Player team.</param>
	/// <returns>Pawn GameObject.</returns>
	private void RespawnPlayer( Connection connection, string tag = "seekers" )
	{
		var startLocation = _networkComponent.FindSpawnLocation().WithScale( 1 );
		// Spawn this object and make the client the owner
		var player = _networkComponent.PlayerPrefab.Clone( startLocation, name: $"Player - {connection?.DisplayName}" );
		player.Tags.Add( tag );
 
		if ( tag == "seekers" )
		{
		}
		player.NetworkSpawn( connection );
	}
}
