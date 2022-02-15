using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceGame
{
	public class MainMenuBehaviour : MonoBehaviour
	{
		public const int GameSceneId = 1;

		public void StartGame()
		{
			SceneManager.LoadScene(GameSceneId);
			Time.timeScale = 1f;
		}

		public void QuitGame()
		{
			Application.Quit();
		}
	}
}
