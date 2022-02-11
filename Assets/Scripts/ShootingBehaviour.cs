using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    private IInputHandler inputHandler;

    [Header("Ammo Lists")]
    [Tooltip("This is used to select the currently Used Ammo")]
    [SerializeField] int primaryUsedAmmo = 0;
    [SerializeField] public List<AmmoSO> primaryAmmoSO;
    [Tooltip("This is used to select the currently Used Secondary Ammo")]
    [SerializeField] int secondaryUsedAmmo = 0;
    [SerializeField] public List<AmmoSO> secondaryAmmoSO;

    [Header("Time")]
    [Tooltip("Defines the time in between two primary shoots")]
    [SerializeField] float shootPeriod = 0.0f;
    private float timeToNextShoot = 0.0f;
    
    [Header("Variables")]
    [SerializeField] private int secondaryAmmoAmount = 0;

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
        if(timeToNextShoot <= 0.0f)
        {
            //Get ammo from Pool
            AmmoBehaviour newAmmo = PrimaryAmmoPool();
            newAmmo.Init(this, transform, primaryAmmoSO[primaryUsedAmmo]);
            newAmmo.GetComponent<Rigidbody2D>().AddForce(Vector2.up * primaryAmmoSO[primaryUsedAmmo].Speed, ForceMode2D.Force);
            timeToNextShoot = shootPeriod;
        }
    }
    private AmmoBehaviour PrimaryAmmoPool()
    {
        if (primaryAmmoPool.Count == 0)
        {
            return Instantiate(primaryAmmoSO[primaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>();
        }

        return primaryAmmoPool.Pop();
    }

    private void FireSecondary()
    {
        if(secondaryAmmoAmount <= 0)
        {
            return;
        }
        AmmoBehaviour newAmmo = SecondaryAmmoPool();
        newAmmo.Init(this, transform, secondaryAmmoSO[secondaryUsedAmmo]);
        newAmmo.GetComponent<Rigidbody2D>().AddForce(Vector2.up * secondaryAmmoSO[secondaryUsedAmmo].Speed, ForceMode2D.Force);
        secondaryAmmoAmount--;
    }

    private AmmoBehaviour SecondaryAmmoPool()
    {
        if (secondaryAmmoPool.Count == 0)
        {
            return Instantiate(secondaryAmmoSO[secondaryUsedAmmo].AmmoObject).GetComponent<AmmoBehaviour>();
        }

        return secondaryAmmoPool.Pop();
    }

    public void TakeBack(AmmoBehaviour ammo)
    {
        ammo.gameObject.SetActive(false);
        if(ammo.AmmoType == 0)
        {
            primaryAmmoPool.Push(ammo);
        }
        if(ammo.AmmoType == 1)
        {
            secondaryAmmoPool.Push(ammo);
        }
    }

}
