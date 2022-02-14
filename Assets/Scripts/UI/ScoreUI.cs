using TMPro;
using UnityEngine;

namespace SpaceGame.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private int score = 0;
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            score = 0;
        }

        void Start()
        {
            text.text = score.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = score.ToString();
        }
        public void IncreaseScore()
        {
            score++;
        }
    }
}
