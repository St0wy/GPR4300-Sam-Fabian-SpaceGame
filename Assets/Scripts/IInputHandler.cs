using UnityEngine;

namespace SpaceGame
{
	public interface IInputHandler
	{
		public delegate void InputEventHandler();

		public Vector2 InputVector { get; }

		event InputEventHandler FirePrimary;
		event InputEventHandler FireSecondary;
		event InputEventHandler Pause;
	}
}
