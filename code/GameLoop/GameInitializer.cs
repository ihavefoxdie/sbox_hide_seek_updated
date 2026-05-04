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

			GameObject ganeManager = Scene.CreateObject();
			ganeManager.Name = "GameLoop";
			ganeManager.AddComponent<GameManager>();

			Settings = new UserGameSettings();
			Settings.LoadSettings();
			MapIdent = Settings.MapName;

			Package info = await Package.FetchAsync( Settings.MapName, true );
			MapIdent = info?.TypeName == "map" ? info.FullIdent : "facepunch.flatgrass";
			
			_loadingScreen = Scene.GetAll<GameLoadingScreen>().FirstOrDefault();
			_loadingScreen?.LoadingElement = MapIdent;

			await Task.Delay( 5000 );

			GameObject map = Scene.CreateObject();
			map.Name = "Map";
			MapInstance instance = map.AddComponent<MapInstance>();
			instance.MapName = MapIdent;
			instance.OnMapLoaded += async () =>
			{
				Log.Info( "Map has been loaded!" );
				bool loaded = await LevelLoader.TransitionToSceneAsync( _gameScenePath, true, Scene );
				if ( !loaded )
				{
					Log.Error( $"Failed to load {_gameScenePath}." );
					LevelLoader.ChangeScene( _mainMenuScenePath, false );
				}
				_loadingScreen?.GameObject?.Destroy();
			};
		}
		catch ( Exception ex )
		{
			Log.Error( $"Failed to initialize the game: {ex}" );
			LevelLoader.ChangeScene( _mainMenuScenePath, false );
		}
	}
}
