using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame.Player
{
	[RequireComponent(typeof(Health))]
	public class HurtSoundBehaviour : MonoBehaviour
	{
		[SerializeField] private SoundEffectScritableObject hurtSound;

		private Health health;

		private void Awake()
		{
			health = GetComponent<Health>();
			health.OnHurt += healthPoints => hurtSound.Play();
		}
	}
}
