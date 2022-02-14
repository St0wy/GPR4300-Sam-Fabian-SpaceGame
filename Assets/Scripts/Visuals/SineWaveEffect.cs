using UnityEngine;

namespace SpaceGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SineWaveEffect : MonoBehaviour
    {

        [SerializeField] private bool enable;

        public float period = 2.0f;
        private float alphaFactor;
        private Color color;
        Color originalColor;

        #region Properties

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        #endregion

        void Start()
        {
            color = GetComponent<SpriteRenderer>().color;
            originalColor = color;
        }

        void Update()
        {
            if (enable)
            {
                SineEffect();
            }
            if (!enable)
            {
                GetComponent<SpriteRenderer>().color = originalColor;
            }
            
        }
        private void SineEffect()
        {
            if (period <= Mathf.Epsilon) { return; }
            float cycle = Time.time / period;

            const float tau = Mathf.PI * 2.0f;
            float sineWave = Mathf.Sin(cycle * tau);
            alphaFactor = ((sineWave + 1.0f) / 2.0f); //SineWave = -1 to 1 // +1 to go from 0 to 2 // divided by 2 for 0 to 1

            color.a = alphaFactor;
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}
