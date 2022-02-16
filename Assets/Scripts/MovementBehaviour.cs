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
			Vector3 movement = inputHandler.InputVector * (speed * Time.deltaTime);

            if (this.CompareTag("Player"))
            {
				var pos = Camera.main.WorldToViewportPoint(transform.position);
				pos.x = Mathf.Clamp(pos.x, 0.07f, 0.93f);
				pos.y = Mathf.Clamp(pos.y, 0.07f, 0.93f);
				transform.position = Camera.main.ViewportToWorldPoint(pos);
			}
			
			transform.position += movement;
		}

		private void Pause()
		{
			Debug.Log("C'est la pause");
		}
	}
		
}
