using UnityEngine;

namespace SpaceGame.ScriptableObjects
{
	[CreateAssetMenu(fileName = "defaultAmmo", menuName = "Ammo")]
	public class AmmoScriptableObject : ScriptableObject
	{
		[SerializeField] private GameObject ammoObject;
		[SerializeField] private int damage;
		[SerializeField] private float speed;
		[SerializeField] private float disableTimer = 5.0f;
		[SerializeField] private AmmoType ammoType;

		#region Properties

		public GameObject AmmoObject => ammoObject;
		public int Damage => damage;
		public float Speed => speed;
		public float DisableTimer => disableTimer;
		public AmmoType AmmoType => ammoType;

		#endregion
	}
}
