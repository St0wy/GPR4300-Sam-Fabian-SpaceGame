using UnityEngine;

namespace SpaceGame.Visuals
{
	[RequireComponent(typeof(Health))]
	public class VFXManager : MonoBehaviour
	{
		private ParticleSystem particles;
		private Health health;

		private void Awake()
		{
			health = GetComponent<Health>();
			particles = GetComponentInChildren<ParticleSystem>();

			health.OnHurt += (healthpoints) => TriggerHurtVfx();
		}

		public void TriggerHurtVfx()
		{
			particles.Play();
		}
	}
}
