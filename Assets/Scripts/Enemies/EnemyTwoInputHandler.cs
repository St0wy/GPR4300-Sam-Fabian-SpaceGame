using MyBox;
using UnityEngine;

namespace SpaceGame.Enemies
{
	public class EnemyTwoInputHandler : MonoBehaviour, IInputHandler
	{
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

		private float shootingTimer;

		private EnemyTwoState state;
		private float NextShootTime => Random.Range(timeBetweenShoot.Min, timeBetweenShoot.Max);

		public Transform[] Lines
		{
			get => lines;
			set => lines = value;
		}

		private void Awake()
		{
			InputVector = new Vector2(0, -1);
			state = EnemyTwoState.Falling;
			shootingTimer = NextShootTime;
		}

		private void Start()
		{
			chosenLineIndex = Random.Range(0, Lines.Length - 1);
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

		private void OnDrawGizmos()
		{
			foreach (Transform line in Lines)
			{
				float y = line.position.y;
				Gizmos.DrawLine(new Vector3(-10, y), new Vector3(10, y));
			}
		}

		public event IInputHandler.InputEventHandler FirePrimary;
		public event IInputHandler.InputEventHandler FireSecondary;
		public event IInputHandler.InputEventHandler Pause;

		public Vector2 InputVector { get; private set; }

		private void HandleShoot()
		{
			shootingTimer -= Time.deltaTime;

			if (!(shootingTimer <= 0f)) return;

			FirePrimary?.Invoke();
			shootingTimer += NextShootTime;
		}

		private void HandleFallingState()
		{
			// Check if we hit the chosen line
			if (lines.Length < 1) return;
			if (!(transform.position.y <= Lines[chosenLineIndex].position.y)) return;

			state = EnemyTwoState.ShootingRight;
			InputVector = new Vector2(1, 0);
		}
	}
}
