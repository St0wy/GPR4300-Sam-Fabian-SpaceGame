using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame.Items
{
	/// <summary>
	/// Script to put on any object that has to drop "loot"
	/// </summary>
	[RequireComponent(typeof(Health))]
	public class ItemDrop : MonoBehaviour
	{
		[SerializeField] private Health health;
		[SerializeField] private List<Item> items;


		private void Awake()
		{
			health = GetComponent<Health>();
		}

		// Update is called once per frame
		private void Update()
		{
			float random = Random.value;

			if (health.HealthPoints > 0) return;

			if (items.Count != 0 && random >= 0.5f)
			{
				Instantiate(items[0]);
			}
		}
	}
}
