using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int refillAmount = 1;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log("Collision");
            if(collider.tag == "Player")
            {
                collider.GetComponent<ShootingBehaviour>().FillSecondaryAmmo(refillAmount);
                gameObject.SetActive(false);
            }
        }
    }
}
