using System;
using UnityEngine;

public class HurtOnCollision : MonoBehaviour
{
	[SerializeField] private int damage = 3;

	private void OnCollisionEnter2D(Collision2D col)
	{
		var oponentHealth = col.gameObject.GetComponent<Health>();

		oponentHealth.ReduceHealth(damage);
	}
}
