using MyBox;
using SpaceGame.Visuals;
using UnityEngine;

namespace SpaceGame
{
	/// <summary>
	/// Script to put on gameobjects that must have health like the player or enemies.
	/// </summary>
	public class Health : MonoBehaviour
	{
		[InitializationField]
		[SerializeField]
		private int maxHealthPoints = 15;

		[SerializeField] private bool destroyWhenKilled = true;

		[ConditionalField(nameof(destroyWhenKilled))]
		[SerializeField]
		private float destroyTime;

		[Tooltip("Field indicating whether the object has invulnerability frames after getting hit.")]
		[SerializeField]
		private bool hasIFrames;

		[ConditionalField(nameof(hasIFrames))]
		[Tooltip("The duration of the invulnerability.")]
		[SerializeField]
		private float iFramesDuration = 0.5f;

		[ReadOnly]
		[SerializeField]
		private float iFrameTimer;

		private InvulFramesEffect invulFramesEffect;

		public delegate void HurtCallback(int healthPoints);

		public HurtCallback OnHurt { get; set; }
		public bool IsAlive { get; private set; }
		public int HealthPoints { get; private set; }
		public bool IsInIFrame => hasIFrames && iFrameTimer > 0;

		private void Awake()
		{
			invulFramesEffect = GetComponent<InvulFramesEffect>();
			HealthPoints = maxHealthPoints;
			IsAlive = true;
		}

		private void Update()
		{
			// Check if IFrames are enabled
			if (!hasIFrames) return;

			// If we have an effect component, update his activation
			if (invulFramesEffect != null)
			{
				invulFramesEffect.IsActive = IsInIFrame;
			}

			// Update the timer if we are in an IFrame
			if (IsInIFrame)
			{
				iFrameTimer -= Time.deltaTime;
			}
		}

		/// <summary>
		/// Reduces the health of the gameobject and kills it if health = 0.
		/// </summary>
		/// <param name="ammount">The ammount of health to substract. Defaults to 1.</param>
		public void ReduceHealth(int ammount = 1)
		{
			if (hasIFrames)
			{
				// Check if the I Frame timer is running, if yes, we quit
				if (IsInIFrame)
				{
					return;
				}

				// Set the I Frame timer to make the entity invulnerable
				iFrameTimer = iFramesDuration;
			}

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
