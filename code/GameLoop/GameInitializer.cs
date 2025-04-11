using HideAndSeek.GameLoop.Rules;
using HideAndSeek.UI;
using HideAndSeek.UI.MainMenu;
using System.Threading.Tasks;



namespace HideAndSeek.GameLoop;

public class GameInitializer : GameObjectSystem
{
#if DEBUG
	private static bool s_bDoDebugOnlyCode = true;
#endif

	//private bool _disposed = false;

	private GameLoadingScreen _loadingScreen;
	public UserGameSettings Settings { get; private set; }
	[Property][Sync] public string MapIdent { get; private set; }

	public GameInitializer( Scene scene ) : base( scene )
	{
		Listen( Stage.SceneLoaded, 11, Initialize, "GameInitialization" );
	}

	/*~GameInitializer()
	{
		Log.Info( $"{this.GetType().ToString()} is disposed!" );
		Dispose();
	}

	public override void Dispose()
	{
		if ( _disposed )
		{
			return;
		}

		_disposed = true;
		GC.SuppressFinalize( this );

		base.Dispose();
	}*/

	async void Initialize()
	{
		if ( Scene.GetAll<MainMenuUI>().Any() || Scene.GetAll<GameManager>().Any() || Scene.IsEditor )
		{
#if DEBUG
			Log.Warning( $"Not a loading scene. Skipping {this}" );
#endif
			return;
		}

#if DEBUG
		Log.Warning( "Loading screen scene detected." );
#endif

		GameObject gameLoop = Scene.CreateObject();
		gameLoop.Name = "GameLoop";
		gameLoop.AddComponent<GameManager>();

		_loadingScreen = Scene.GetAll<GameLoadingScreen>().FirstOrDefault();
		Settings = new UserGameSettings();
		Settings.LoadSettings();

#if DEBUG
		Log.Info( Settings.MapName );
#endif

		MapIdent = Settings.MapName;


		Task<Package> task = Package.Fetch( Settings.MapName, true );
		task.Wait();
		Package info = task.Result;
		if ( info != null )
		{
			if ( info.TypeName == "map" )
			{
				MapIdent = info.FullIdent;
			}
		}

		_loadingScreen.LoadingElement = MapIdent;

		await Task.Delay( 5000 );
		GameObject map = Scene.CreateObject();
		map.Name = "Map";
		MapInstance instance = map.AddComponent<MapInstance>();
		instance.MapName = MapIdent;
		instance.OnMapLoaded += async () =>
		{
			Log.Info( "Map has been loaded!" );
			await LevelLoader.ChangeScene( "scenes/GameScene.scene", true, Scene );
			_loadingScreen?.GameObject?.Destroy();
		};
	}
}
