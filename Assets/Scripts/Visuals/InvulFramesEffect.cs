using MyBox;
using UnityEngine;

namespace SpaceGame.Visuals
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class InvulFramesEffect : MonoBehaviour
	{
		[SerializeField] private bool isActive = true;

		[ConditionalField(nameof(isActive))]
		[SerializeField]
		private float period = 0.25f;

		private float alphaFactor;
		private Color color;
		private Color originalColor;

		public bool IsActive
		{
			get => isActive;
			set => isActive = value;
		}

		private void Start()
		{
			color = GetComponent<SpriteRenderer>().color;
			originalColor = color;
		}

		private void Update()
		{
			if (isActive)
			{
				InvulEffect();
			}
			else
			{
				GetComponent<SpriteRenderer>().color = originalColor;
			}
		}

		private void InvulEffect()
		{
			if (period <= Mathf.Epsilon) return;

			float cycle = Time.time / period;
			const float tau = Mathf.PI * 2.0f;
			float sineWave = Mathf.Sin(cycle * tau);
			alphaFactor =
				(sineWave + 1.0f) / 2.0f; //SineWave = -1 to 1 // +1 to go from 0 to 2 // divided by 2 for 0 to 1

			color.a = alphaFactor;
			GetComponent<SpriteRenderer>().color = color;
		}
	}
}
