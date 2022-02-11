using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehaviour : MonoBehaviour
{
    private ShootingBehaviour shooter;
    [SerializeField] private float disableTimer = Mathf.Infinity;
    
    public void Init(ShootingBehaviour shooter, Transform spawnPos, AmmoSO ammoSO)
    {
        //Gives the ShootingBehaviour as ref
        this.shooter = shooter;
        //Reset position
        transform.position = spawnPos.position;
        transform.rotation = spawnPos.rotation;

        disableTimer = ammoSO.DisableTimer;

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
            //TODO: Lose hp to enemy
            shooter.TakeBack(this);
        }
    }
}
