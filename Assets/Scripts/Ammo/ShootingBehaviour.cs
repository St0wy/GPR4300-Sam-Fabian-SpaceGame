using System;
using System.Collections.Generic;
using SpaceGame.ScriptableObjects;
using UnityEngine;

namespace SpaceGame.Ammo
{
	public class ShootingBehaviour : MonoBehaviour
	{
		[SerializeField] public List<AmmoScriptableObject> primaryAmmoScriptableObjects;

		[SerializeField] public List<AmmoScriptableObject> secondaryAmmoScriptableObjects;

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
		private float shootPeriod = 0.1f;

		[Header("Variables")]
		[SerializeField]
		private int secondaryAmmoAmount = 10;

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
			newAmmo.Init(this, transform, primaryAmmoScriptableObjects[primaryUsedAmmo]);
			newAmmo.GetComponent<Rigidbody2D>()
				.AddForce(Vector2.up * primaryAmmoScriptableObjects[primaryUsedAmmo].Speed, ForceMode2D.Force);
			timeToNextShoot = shootPeriod;
		}

		private void FireSecondary()
		{
			if (secondaryAmmoAmount <= 0) return;

			AmmoBehaviour newAmmo = SecondaryAmmoPool();
			newAmmo.Init(this, transform, secondaryAmmoScriptableObjects[secondaryUsedAmmo]);
			newAmmo.GetComponent<Rigidbody2D>()
				.AddForce(Vector2.up * secondaryAmmoScriptableObjects[secondaryUsedAmmo].Speed, ForceMode2D.Force);
			secondaryAmmoAmount--;
		}

		private AmmoBehaviour PrimaryAmmoPool()
		{
			return primaryAmmoPool.Count == 0
				? Instantiate(primaryAmmoScriptableObjects[primaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>()
				: primaryAmmoPool.Pop();
		}

		private AmmoBehaviour SecondaryAmmoPool()
		{
			return secondaryAmmoPool.Count == 0
				? Instantiate(secondaryAmmoScriptableObjects[secondaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>()
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

		public void FillSecondaryAmmo(int amount)
        {
			secondaryAmmoAmount += amount;
        }
	}
}
