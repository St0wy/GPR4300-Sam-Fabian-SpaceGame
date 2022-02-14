using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame.UI
{
	public class GameOverUI : MonoBehaviour
	{
		[SerializeField] private Button mainMenuButton;
		[SerializeField] private Health healthComponent;

		private void Awake()
		{
			healthComponent.OnHurt += (healthPoints) =>
			{
				if (healthPoints <= 0)
				{
					TriggerGameOver();
				}
			};
		}

		public void LoadMainMenu()
		{
			Debug.Log("Loading main menu...");

			// SceneManager.LoadScene(0);
		}

		private void TriggerGameOver()
		{
			Time.timeScale = 0f;
			mainMenuButton.Select();
			gameObject.SetActive(true);
		}
	}
}
