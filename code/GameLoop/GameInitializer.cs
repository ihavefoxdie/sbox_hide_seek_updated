using HideAndSeek.GameLoop.Rules;
using HideAndSeek.UI;
using HideAndSeek.UI.MainMenu;
using System;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public class GameInitializer : GameObjectSystem
{
	#region Variables
	private GameLoadingScreen _loadingScreen;
	[Sync(SyncFlags.FromHost)] private MapInstance MapInstance { get; set; }
	private readonly string _gameScenePath = "scenes/GameScene.scene";
	private readonly string _mainMenuScenePath = "scenes/MainMenu.scene";
	#endregion


	#region Properties
	public UserGameSettings Settings { get; private set; }
	[Property][Sync] public string MapIdent { get; private set; }
	#endregion



	public GameInitializer( Scene scene ) : base( scene )
	{
		Listen( Stage.SceneLoaded, 11, Initialize, "GameInitialization" );
	}



	private async void Initialize()
	{
		try
		{
			if ( Scene.GetAll<MainMenuUI>().Any() || Scene.GetAll<GameManager>().Any() || Scene.IsEditor )
			{
				return;
			}

			_loadingScreen = Scene.GetAll<GameLoadingScreen>().FirstOrDefault();

			if ( Networking.IsHost )
			{
				Settings = new UserGameSettings();
				Settings.LoadSettings();
				MapIdent = Settings.MapName;

				Package info = await Package.FetchAsync( Settings.MapName, true );
				MapIdent = info?.TypeName == "map" ? info.FullIdent : "facepunch.flatgrass";

				_loadingScreen?.LoadingElement = MapIdent;

				await Task.Delay( 5000 );

				GameObject map = Scene.CreateObject();
				map.Name = "Map";
				MapInstance = map.AddComponent<MapInstance>();
				MapInstance.MapName = MapIdent;
				MapInstance.OnMapLoaded += async () =>
				{
					Log.Info( "Map has been loaded!" );
					GameObject gameManager = Scene.CreateObject();
					gameManager.Name = "GameLoop";
					gameManager.GetOrAddComponent<GameManager>();
					gameManager.NetworkMode = NetworkMode.Object;
					bool loaded = await LevelLoader.TransitionToSceneAsync( _gameScenePath, true, Scene );
					_loadingScreen?.GameObject?.Destroy();
					if ( !loaded )
					{
						Log.Error( $"Failed to load {_gameScenePath}." );
						LevelLoader.ChangeScene( _mainMenuScenePath, false );
					}
				};
			}
			if ( Networking.IsClient )
			{
				_loadingScreen?.LoadingElement = MapInstance?.MapName;
			}
		}
		catch ( Exception ex )
		{
			Log.Error( $"Failed to initialize the game: {ex}" );
			LevelLoader.ChangeScene( _mainMenuScenePath, false );
		}
	}
}
