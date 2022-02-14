using MyBox;
using UnityEngine;

namespace SpaceGame
{
	/// <summary>
	/// Script to put on gameobjects that must have health like the player or enemies.
	/// </summary>
	public class Health : MonoBehaviour
	{
		[SerializeField] private int maxHealthPoints = 15;
		[SerializeField] private bool destroyWhenKilled = true;

		[ConditionalField(nameof(destroyWhenKilled))]
		[SerializeField]
		private float destroyTime;

		public delegate void HurtCallback(int healthPoints);

		public HurtCallback OnHurt { get; set; }
		public bool IsAlive { get; private set; }
		public int HealthPoints { get; private set; }

		private void Awake()
		{
			HealthPoints = maxHealthPoints;
			IsAlive = true;
		}

		/// <summary>
		/// Reduces the health of the gameobject and kills it if health = 0.
		/// </summary>
		/// <param name="ammount">The ammount of health to substract. Defaults to 1.</param>
		public void ReduceHealth(int ammount = 1)
		{
			HealthPoints -= ammount;

			if (HealthPoints <= 0)
			{
				IsAlive = false;
			}

			OnHurt?.Invoke(HealthPoints);

			if (!IsAlive && destroyWhenKilled)
			{
				Destroy(gameObject, destroyTime);
			}
		}
	}
}
