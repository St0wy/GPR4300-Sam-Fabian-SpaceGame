using UnityEngine;

namespace SpaceGame
{
	public static class MathHelpers
	{
		public static Vector2 RadianToVector2(float radian) => new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

		public static Vector2 DegreeToVector2(float degree) => RadianToVector2(degree * Mathf.Deg2Rad);
	}
}
