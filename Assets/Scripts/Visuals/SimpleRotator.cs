using UnityEngine;

namespace SpaceGame.Visuals
{
    public class SimpleRotator : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed);        
        }
    }
}
