using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class VFXManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem ps;
        [SerializeField] private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
            ps = GetComponentInChildren<ParticleSystem>();

            health.OnHurt += (healthpoints) =>GetsHit();
        }

        public void GetsHit()
        {
            ps.Play();
        }
        
    }
}
