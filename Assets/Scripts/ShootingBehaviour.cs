using System;
using System.Collections.Generic;
using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame
{
	public class ShootingBehaviour : MonoBehaviour
	{
		[SerializeField] public List<AmmoSO> primaryAmmoSO;

		[SerializeField] public List<AmmoSO> secondaryAmmoSO;

		[Header("Ammo Lists")]
		[Tooltip("This is used to select the currently Used Ammo")]
		[SerializeField]
		private int primaryUsedAmmo;

		[Tooltip("This is used to select the currently Used Secondary Ammo")]
		[SerializeField]
		private int secondaryUsedAmmo;

		[Header("Time")]
		[Tooltip("Defines the time in between two primary shoots")]
		[SerializeField]
		private float shootPeriod;

		[Header("Variables")]
		[SerializeField]
		private int secondaryAmmoAmount;

		private float timeToNextShoot;
		private IInputHandler inputHandler;
		private Stack<AmmoBehaviour> primaryAmmoPool;
		private Stack<AmmoBehaviour> secondaryAmmoPool;

		private void Awake()
		{
			inputHandler = GetComponent<IInputHandler>();
			inputHandler.FirePrimary += FirePrimary;
			inputHandler.FireSecondary += FireSecondary;

			primaryAmmoPool = new Stack<AmmoBehaviour>();
			secondaryAmmoPool = new Stack<AmmoBehaviour>();
		}

		private void Update()
		{
			timeToNextShoot -= Time.deltaTime;
		}

		private void FirePrimary()
		{
			if (!(timeToNextShoot <= 0.0f)) return;

			//Get ammo from Pool
			AmmoBehaviour newAmmo = PrimaryAmmoPool();
			newAmmo.Init(this, transform, primaryAmmoSO[primaryUsedAmmo]);
			newAmmo.GetComponent<Rigidbody2D>()
				.AddForce(Vector2.up * primaryAmmoSO[primaryUsedAmmo].Speed, ForceMode2D.Force);
			timeToNextShoot = shootPeriod;
		}

		private void FireSecondary()
		{
			if (secondaryAmmoAmount <= 0) return;

			AmmoBehaviour newAmmo = SecondaryAmmoPool();
			newAmmo.Init(this, transform, secondaryAmmoSO[secondaryUsedAmmo]);
			newAmmo.GetComponent<Rigidbody2D>()
				.AddForce(Vector2.up * secondaryAmmoSO[secondaryUsedAmmo].Speed, ForceMode2D.Force);
			secondaryAmmoAmount--;
		}

		private AmmoBehaviour PrimaryAmmoPool()
		{
			return primaryAmmoPool.Count == 0
				? Instantiate(primaryAmmoSO[primaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>()
				: primaryAmmoPool.Pop();
		}

		private AmmoBehaviour SecondaryAmmoPool()
		{
			return secondaryAmmoPool.Count == 0
				? Instantiate(secondaryAmmoSO[secondaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>()
				: secondaryAmmoPool.Pop();
		}

		public void TakeBack(AmmoBehaviour ammo)
		{
			ammo.gameObject.SetActive(false);
			switch (ammo.AmmoType)
			{
				case AmmoType.Primary:
					primaryAmmoPool.Push(ammo);
					break;
				case AmmoType.Secondary:
					secondaryAmmoPool.Push(ammo);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
