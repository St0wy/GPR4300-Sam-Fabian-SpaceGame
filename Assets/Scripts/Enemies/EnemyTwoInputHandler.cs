using MyBox;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpaceGame.Enemies
{
	public class EnemyTwoInputHandler : MonoBehaviour, IInputHandler
	{
		public event IInputHandler.InputEventHandler FirePrimary;
		public event IInputHandler.InputEventHandler FireSecondary;
		public event IInputHandler.InputEventHandler Pause;

		[SerializeField] private Transform[] lines;

		[Attributes.ReadOnly]
		[SerializeField]
		private int chosenLineIndex;

		[Tooltip("The horizontal size of the area where the enemy can be.")]
		[SerializeField]
		private RangedFloat horizontalLimit = new RangedFloat(-8f, 8f);

		[Tooltip("The time that the enemy spends moving between two shots.")]
		[SerializeField]
		private RangedFloat timeBetweenShoot = new RangedFloat(0.5f, 1.5f);

		private EnemyTwoState state;
		private float shootingTimer;

		public Vector2 InputVector { get; private set; }
		private float NextShootTime => Random.Range(timeBetweenShoot.Min, timeBetweenShoot.Max);

		private void Awake()
		{
			InputVector = new Vector2(0, -1);
			state = EnemyTwoState.Falling;
			chosenLineIndex = Random.Range(0, lines.Length);
			shootingTimer = NextShootTime;
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
					if (transform.position.x >= horizontalLimit.Max)
					{
						state = EnemyTwoState.ShootingLeft;
						InputVector = new Vector2(-1, 0);
					}

					HandleShoot();
					break;
				case EnemyTwoState.ShootingLeft:
					if (transform.position.x <= horizontalLimit.Min)
					{
						state = EnemyTwoState.ShootingRight;
						InputVector = new Vector2(1, 0);
					}

					HandleShoot();
					break;
			}
		}

		private void HandleShoot()
		{
			shootingTimer -= Time.deltaTime;

			if (!(shootingTimer <= 0f)) return;
			
			FirePrimary?.Invoke();
			shootingTimer += NextShootTime;
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
