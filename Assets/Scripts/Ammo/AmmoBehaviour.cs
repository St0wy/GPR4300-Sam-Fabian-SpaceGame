using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame.Ammo
{
	public class AmmoBehaviour : MonoBehaviour
	{
		[SerializeField] private float disableTimer = Mathf.Infinity;
		[SerializeField] private int damage;
		[SerializeField] private AmmoType ammoType = AmmoType.Primary;

		private ShootingBehaviour shooter;

		public AmmoType AmmoType => ammoType;

		private void Update()
		{
			disableTimer -= Time.deltaTime;

			if (disableTimer <= 0.0f)
			{
				shooter.TakeBack(this);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var health = other.GetComponent<Health>();

			if (health == null || health.IsInIFrame) return;

			// Pools the object back to the ShootingBehaviour ammoPool
			shooter.TakeBack(this);

			// Activates damage according to the AmmoSO
			health.ReduceHealth(damage);
		}

		public void Init(ShootingBehaviour shootingBehaviour, Transform spawnPos,
			AmmoScriptableObject ammoScriptableObject)
		{
			// Gives the ShootingBehaviour as ref
			shooter = shootingBehaviour;

			// Set position and rotation on the bullet
			Transform myTransform = transform; // Access the transform only once to be more performant
			myTransform.position = spawnPos.position;
			myTransform.rotation = spawnPos.rotation;

			// Set Main variables
			damage = ammoScriptableObject.Damage;
			disableTimer = ammoScriptableObject.DisableTimer;
			ammoType = ammoScriptableObject.AmmoType;

			// Enable object once placed
			gameObject.SetActive(true);
		}
	}
}
