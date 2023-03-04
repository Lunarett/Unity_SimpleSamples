using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text healthValueText;
    [SerializeField] private Slider slider;

    public void SetSliderValue(float value)
    {
        slider.value = value;
        healthValueText.text = $"{(int)(value * 100)}%";
    }
}
