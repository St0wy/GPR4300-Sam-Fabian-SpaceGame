using System;
using UnityEngine;

public class HurtOnCollision : MonoBehaviour
{
	[SerializeField] private int damage = 3;

	private void OnCollisionEnter2D(Collision2D col)
	{
		var oponentHealth = col.gameObject.GetComponent<Health>();
		if (oponentHealth == null) return;

		oponentHealth.ReduceHealth(damage);
	}
}
