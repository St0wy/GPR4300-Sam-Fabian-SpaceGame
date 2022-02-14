using SpaceGame.Ammo;
using UnityEngine;

namespace SpaceGame.Items
{
	public class Item : MonoBehaviour
	{
		[SerializeField] private int refillAmount = 1;

		private void OnTriggerEnter(Collider col)
		{
			Debug.Log("Collision");
			if (!col.CompareTag("Player")) return;
			
			col.GetComponent<ShootingBehaviour>().FillSecondaryAmmo(refillAmount);
			gameObject.SetActive(false);
		}
	}
}
