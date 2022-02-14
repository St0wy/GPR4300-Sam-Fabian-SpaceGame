using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed);        
        }
    }
}
