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
		[SerializeField] private List<Item> items;

		[Tooltip("Chance to drop an item in percentages where 0 = 0% and 100 = 100%.")]
		[SerializeField]
		private float chanceToDrop = 10f;

		private Health health;

		private void Awake()
		{
			health = GetComponent<Health>();
			health.OnHurt += OnHurt;
		}

		private void OnHurt(int healthPoints)
		{
			// Check if the entity is dead
			if (healthPoints > 0) return;

			// Check if we match the chances
			if (!(Random.Range(0, 100) <= chanceToDrop)) return;
			
			Instantiate(items[Random.Range(0, items.Count)], transform.position,
				transform.rotation);
		}
	}
}
