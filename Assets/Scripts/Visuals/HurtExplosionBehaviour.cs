using UnityEngine;

namespace SpaceGame.Visuals
{
	[RequireComponent(typeof(Health))]
	public class HurtExplosionBehaviour : MonoBehaviour
	{
		private Health health;
		private ParticleSystem particles;

		private void Awake()
		{
			health = GetComponent<Health>();
			particles = GetComponentInChildren<ParticleSystem>();

			health.OnHurt += healthpoints => TriggerHurtVfx();
		}

		public void TriggerHurtVfx()
		{
			particles.Play();
		}
	}
}
