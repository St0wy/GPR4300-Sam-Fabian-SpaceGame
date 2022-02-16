using System;
using SpaceGame.Ammo;
using TMPro;
using UnityEngine;

namespace SpaceGame.UI
{
	public class SecondaryAmmoUI : MonoBehaviour
	{
		[SerializeField] private ShootingBehaviour shootingBehaviour;
		private TextMeshProUGUI text;

		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			text.text = $"Ammos : {shootingBehaviour.SecondaryAmmoAmount}";
		}
	}
}
