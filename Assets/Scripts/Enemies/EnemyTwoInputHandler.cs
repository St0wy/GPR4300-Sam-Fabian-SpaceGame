using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame.Enemies
{
	public class EnemyTwoInputHandler : MonoBehaviour, IInputHandler
	{
		public Vector2 InputVector { get; private set; }
		public event IInputHandler.InputEventHandler FirePrimary;
		public event IInputHandler.InputEventHandler FireSecondary;
		public event IInputHandler.InputEventHandler Pause;

		[SerializeField] private Transform[] lines;

		[ReadOnly]
		[SerializeField]
		private int chosenLineIndex;

		[SerializeField] private float maxPosLeft = -8f;
		[SerializeField] private float maxPosRight = 8f;

		private EnemyTwoState state;

		private void Awake()
		{
			InputVector = new Vector2(0, -1);
			state = EnemyTwoState.Falling;
			chosenLineIndex = Random.Range(0, lines.Length);
		}

		private void Update()
		{
			switch (state)
			{
				default:
				case EnemyTwoState.Falling:
					HandleFallingState();
					break;
				case EnemyTwoState.ShootingRight:
					if (transform.position.x >= maxPosRight)
					{
						state = EnemyTwoState.ShootingLeft;
						InputVector = new Vector2(-1, 0);
					}

					break;
				case EnemyTwoState.ShootingLeft:
					if (transform.position.x <= maxPosLeft)
					{
						state = EnemyTwoState.ShootingRight;
						InputVector = new Vector2(1, 0);
					}

					break;
			}
		}

		private void OnDrawGizmos()
		{
			foreach (Transform line in lines)
			{
				float y = line.position.y;
				Gizmos.DrawLine(new Vector3(-10, y), new Vector3(10, y));
			}
		}

		private void HandleFallingState()
		{
			// Check if we hit the chosen line
			if (!(transform.position.y <= lines[chosenLineIndex].position.y)) return;

			state = EnemyTwoState.ShootingRight;
			InputVector = new Vector2(1, 0);
		}
	}
}
