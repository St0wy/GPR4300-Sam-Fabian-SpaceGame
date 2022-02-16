using UnityEngine;

namespace SpaceGame.Enemies
{
	public class EnemyOneInputHandler : MonoBehaviour, IInputHandler
	{
		private void Awake()
		{
			InputVector = new Vector2(0, -1);
		}

		public Vector2 InputVector { get; private set; }
		public event IInputHandler.InputEventHandler FirePrimary;
		public event IInputHandler.InputEventHandler FireSecondary;
		public event IInputHandler.InputEventHandler Pause;
	}
}
