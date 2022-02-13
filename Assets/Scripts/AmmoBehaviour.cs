using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
    private ShootingBehaviour shooter;
    [SerializeField] private float disableTimer = Mathf.Infinity;
    [SerializeField] private int damage = 0;
    [SerializeField] private int ammoType = -1;

    #region Getter

    public int AmmoType => ammoType;

    #endregion

    public void Init(ShootingBehaviour shooter, Transform spawnPos, AmmoSO ammoSO)
    {
        //Gives the ShootingBehaviour as ref
        this.shooter = shooter;
        //Set position
        transform.position = spawnPos.position;
        transform.rotation = spawnPos.rotation;

        //Set Main variables
        damage = ammoSO.Damage;
        disableTimer = ammoSO.DisableTimer;
        ammoType = ammoSO.AmmoType;

        //Enable object once placed
        gameObject.SetActive(true);
    }
    private void Update()
    {
        disableTimer -= Time.deltaTime;

        if (disableTimer <= 0.0f)
        {
            shooter.TakeBack(this);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //Pools the object back to the ShootingBehaviour ammoPool
            shooter.TakeBack(this);
            //Activates damage according to the AmmoSO
            other.GetComponent<Health>().ReduceHealth(damage);
        }
    }
}
