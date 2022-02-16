using SpaceGame.Ammo;
using UnityEngine;

namespace SpaceGame.Items
{
	/// <summary>
	///     Item that refills the secondary weapon of the player.
	/// </summary>
	public class Item : MonoBehaviour
	{
		[Tooltip("By how much does the item refill the secondary weapon of the player.")]
		[SerializeField]
		private int refillAmount = 1;

		private void OnTriggerEnter2D(Collider2D col)
		{
			if (!col.CompareTag("Player")) return;

			col.GetComponent<ShootingBehaviour>().FillSecondaryAmmo(refillAmount);
			Destroy(gameObject);
		}
	}
}
