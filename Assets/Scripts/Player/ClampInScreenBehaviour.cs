using System;
using MyBox;
using UnityEngine;

namespace SpaceGame.Player
{
	public class ClampInScreenBehaviour : MonoBehaviour
	{
		[SerializeField] private RangedFloat screenXLimit = new RangedFloat(0.07f, 0.93f);
		[SerializeField] private RangedFloat screenYLimit = new RangedFloat(0.07f, 0.93f);

		private Camera mainCam;

		private void Awake()
		{
			mainCam = Camera.main;
		}

		private void Update()
		{
			Vector3 pos = mainCam.WorldToViewportPoint(transform.position);
			pos.x = Mathf.Clamp(pos.x, screenXLimit.Min, screenXLimit.Max);
			pos.y = Mathf.Clamp(pos.y, screenYLimit.Min, screenYLimit.Max);
			transform.position = mainCam.ViewportToWorldPoint(pos);
		}
	}
}
