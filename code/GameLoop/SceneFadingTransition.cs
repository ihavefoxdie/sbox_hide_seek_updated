using HideAndSeek.UI;
using HideAndSeek.Utils.Interfaces;
using System.Threading.Tasks;

namespace HideAndSeek.GameLoop;

public class SceneFadingTransition : GameObjectSystem, IBeforeSceneChangeListener
{
	private BlackScreen _blackScreen;

	public SceneFadingTransition( Scene scene ) : base( scene )
	{
		Listen( Stage.SceneLoaded, 10, AddBlackScreen, "blackScreen" );
	}

	public async Task BeforeChange()
	{
		if ( _blackScreen != null )
		{
			_blackScreen.Opacity = 1;
			await Task.Delay( 1000 );
			GameObject blackScreenObject = _blackScreen.GameObject;
			blackScreenObject.Destroy();
		}
	}

	private void AddBlackScreen()
	{
		if ( Scene.GetAll<BlackScreen>().Any() || Scene.IsEditor )
		{
			return;
		}

		GameObject gameObject = Scene.CreateObject();
		gameObject.Name = "FadingTransition";
		ScreenPanel screenPanel = gameObject.AddComponent<ScreenPanel>();
		screenPanel.ZIndex = 110;
		_blackScreen = gameObject.AddComponent<BlackScreen>();
	}
}
