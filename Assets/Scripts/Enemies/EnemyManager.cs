using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame.Enemies
{
	public class EnemyManager : MonoBehaviour
	{
		[SerializeField] private Transform[] lines;

		[Header("EnemyPrefabs")]
		[SerializeField]
		private GameObject enemyOne;

		[SerializeField] private GameObject enemyTwo;

		[Header("Screen size value")]
		[SerializeField]
		private float minPos = -8.0f;

		[SerializeField] private float maxPos = 8.0f;

		[Header("Timer")]
		[SerializeField]
		private float spawnEnemyOneTimerLimit = 5.0f;

		[SerializeField] private float spawnEnemyTwoTimerLimit = 10.0f;
		[SerializeField] private int nbrEnemiesToKill = 50;
		[SerializeField] private GameObject winScreen;
		[SerializeField] private Button mainMenuButton;

		private float spawnEnemyOneTimer;
		private float spawnEnemyTwoTimer;

		public int NbrEnemiesToKill => nbrEnemiesToKill;

		private void Update()
		{
			spawnEnemyOneTimer += Time.deltaTime;
			spawnEnemyTwoTimer += Time.deltaTime;

			if (spawnEnemyOneTimer >= spawnEnemyOneTimerLimit)
			{
				InstantiateEnemyOne();
				spawnEnemyOneTimer = 0.0f;
			}

			if (!(spawnEnemyTwoTimer >= spawnEnemyTwoTimerLimit)) return;

			InstantiateEnemyTwo();
			spawnEnemyTwoTimer = 0.0f;
		}

		private void InstantiateEnemyOne()
		{
			Vector3 spawnPos = transform.position;
			spawnPos.x = Random.Range(minPos, maxPos);
			GameObject newEnemy = Instantiate(enemyOne, spawnPos, Quaternion.identity);
			newEnemy.GetComponent<Health>().OnHurt += OnHurt;
		}

		private void InstantiateEnemyTwo()
		{
			GameObject newEnemy = Instantiate(enemyTwo);
			newEnemy.GetComponent<EnemyTwoInputHandler>().Lines = lines;
			newEnemy.GetComponent<Health>().OnHurt += OnHurt;
		}

		private void OnHurt(int healthPoints)
		{
			if (healthPoints > 0) return;

			nbrEnemiesToKill--;

			if (nbrEnemiesToKill <= 0)
			{
				ShowWinScreen();
			}
		}

		private void ShowWinScreen()
		{
			winScreen.SetActive(true);
			mainMenuButton.Select();
			Time.timeScale = 0f;
		}
	}
}
