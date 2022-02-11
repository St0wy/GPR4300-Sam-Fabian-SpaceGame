using TMPro;
using UnityEngine;

namespace UI
{
	public class HealthUI : MonoBehaviour
	{
		[SerializeField] private Health healthComponent;
		private TextMeshProUGUI text;

		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			text.text = $"Life : {healthComponent.HealthPoints}";
		}
	}
}
