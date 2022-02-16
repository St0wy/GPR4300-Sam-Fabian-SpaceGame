using System;
using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame
{
	[RequireComponent(typeof(Health))]
	public class ExplosionOnDeathBehaviour : MonoBehaviour
	{
		[SerializeField] private SoundEffectScritableObject explosionSound;
		[SerializeField] private ParticleSystem particles;
		private Health health;

		private void Awake()
		{
			particles = GetComponent<ParticleSystem>();
			health = GetComponent<Health>();
			health.OnHurt += (healthPoints) =>
			{
				if (healthPoints <= 0)
				{
					OnDeath();
				}
			};
		}

		private void OnDeath()
		{
			explosionSound.Play();
			particles.Play();
		}
	}
}
