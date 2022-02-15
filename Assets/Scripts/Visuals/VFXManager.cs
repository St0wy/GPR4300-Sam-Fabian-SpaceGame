using UnityEngine;
using UnityEngine.Serialization;

namespace SpaceGame.Visuals
{
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(ParticleSystem))]
	public class VFXManager : MonoBehaviour
	{
		[FormerlySerializedAs("ps")]
		[SerializeField]
		private ParticleSystem particles;

		[SerializeField] private Health health;

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
