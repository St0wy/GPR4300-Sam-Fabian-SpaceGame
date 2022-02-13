using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
	private IInputHandler inputHandler;

	[Header("Scriptable Object references")]
	[SerializeField]
	public List<AmmoSO> ammoSO;

	[Header("Time")] [SerializeField] private float shootPeriod = 0.0f;
	private float timeToNextShoot = 0.0f;

	private Stack<AmmoBehaviour> ammoPool;
	[SerializeField] private int primaryAmmoAmount = 1;
	[SerializeField] private int secondaryAmmoAmount = 0;

	private void Awake()
	{
		inputHandler = GetComponent<IInputHandler>();
		inputHandler.FirePrimary += FirePrimary;
		inputHandler.FireSecondary += FireSecondary;

		ammoPool = new Stack<AmmoBehaviour>();
	}

	private void Update()
	{
		timeToNextShoot -= Time.deltaTime;
	}

	private void FirePrimary()
	{
		if (!(timeToNextShoot <= 0.0f)) return;

		// Get ammo from Pool
		AmmoBehaviour newAmmo = PoolPrimaryAmmo();
		newAmmo.Init(this, transform, ammoSO[0]);
		newAmmo.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ammoSO[0].Speed, ForceMode2D.Force);
		timeToNextShoot = shootPeriod;
	}

	private void FireSecondary()
	{
		if (secondaryAmmoAmount <= 0) return;

		AmmoBehaviour newAmmo = PoolSecondaryAmmo();
		newAmmo.Init(this, transform, ammoSO[1]);
		newAmmo.GetComponent<Rigidbody2D>().AddForce(Vector2.up * ammoSO[1].Speed, ForceMode2D.Force);
		secondaryAmmoAmount--;
	}

	private AmmoBehaviour PoolPrimaryAmmo() => ammoPool.Count == 0
		? Instantiate(ammoSO[0].AmmoObject).GetComponent<AmmoBehaviour>()
		: ammoPool.Pop();

	private AmmoBehaviour PoolSecondaryAmmo() => ammoPool.Count == 0
		? Instantiate(ammoSO[1].AmmoObject).GetComponent<AmmoBehaviour>()
		: ammoPool.Pop();

	public void TakeBack(AmmoBehaviour ammo)
	{
		ammo.gameObject.SetActive(false);
		ammoPool.Push(ammo);
	}
}
