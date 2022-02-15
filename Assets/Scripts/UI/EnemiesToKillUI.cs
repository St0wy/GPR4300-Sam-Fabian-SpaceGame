using System;
using SpaceGame.Enemies;
using TMPro;
using UnityEngine;

namespace SpaceGame.UI
{
	public class EnemiesToKillUI : MonoBehaviour
	{
		[SerializeField] private EnemyManager enemyManager;
		private TextMeshProUGUI text;

		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			text.text = $"Enemies to kill : {enemyManager.NbrEnemiesToKill}";
		}
	}
}
