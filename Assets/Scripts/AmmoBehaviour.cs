using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
	private ShootingBehaviour shooter;
	[SerializeField] private float disableTimer = Mathf.Infinity;
	[SerializeField] private int damage = 0;
	[SerializeField] private AmmoType ammoType = AmmoType.Primary;

	public AmmoType AmmoType => ammoType;

	public void Init(ShootingBehaviour shootingBehaviour, Transform spawnPos, AmmoSO ammoSO)
	{
		// Gives the ShootingBehaviour as ref
		shooter = shootingBehaviour;

		// Set position
		Transform myTransform = transform;
		myTransform.position = spawnPos.position;
		myTransform.rotation = spawnPos.rotation;

		// Set Main variables
		damage = ammoSO.Damage;
		disableTimer = ammoSO.DisableTimer;
		ammoType = ammoSO.AmmoType;

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
