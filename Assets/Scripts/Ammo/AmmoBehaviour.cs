using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame.Ammo
{
	public class AmmoBehaviour : MonoBehaviour
	{
		private ShootingBehaviour shooter;
		[SerializeField] private float disableTimer = Mathf.Infinity;
		[SerializeField] private int damage;
		[SerializeField] private AmmoType ammoType = AmmoType.Primary;

		public AmmoType AmmoType => ammoType;

		public void Init(ShootingBehaviour shootingBehaviour, Transform spawnPos, AmmoScriptableObject ammoScriptableObject)
		{
			// Gives the ShootingBehaviour as ref
			shooter = shootingBehaviour;

			// Set position
			Transform myTransform = transform;
			myTransform.position = spawnPos.position;
			myTransform.rotation = spawnPos.rotation;

			// Set Main variables
			damage = ammoScriptableObject.Damage;
			disableTimer = ammoScriptableObject.DisableTimer;
			ammoType = ammoScriptableObject.AmmoType;

			// Enable object once placed
			gameObject.SetActive(true);
		}

		private void Update()
		{
			disableTimer -= Time.deltaTime;

			if (disableTimer <= 0.0f)
			{
				shooter.TakeBack(this);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag("Enemy")) return;

			// Pools the object back to the ShootingBehaviour ammoPool
			shooter.TakeBack(this);

			// Activates damage according to the AmmoSO
			other.GetComponent<Health>().ReduceHealth(damage);
		}
	}
}
