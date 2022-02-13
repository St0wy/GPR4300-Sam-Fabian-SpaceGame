using UnityEngine;

namespace SpaceGame
{
	public class MovementBehaviour : MonoBehaviour
	{
		[SerializeField] private float speed = 1f;

		private IInputHandler inputHandler;

		private void Awake()
		{
			inputHandler = GetComponent<IInputHandler>();
			inputHandler.Pause += Pause;
		}

		private void FixedUpdate()
		{
			Vector3 movement = inputHandler.InputVector * speed * Time.deltaTime;

			transform.position += movement;
		}

		private void Pause()
		{
			Debug.Log("C'est la pause");
		}
	}
}
