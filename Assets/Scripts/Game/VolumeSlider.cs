using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        slider = GetComponent<Slider>();

        var volume = GameSettings.GameVolume;
        slider.value = volume;
        text.SetText($"Game Volume: {volume * 100f}%");
    }

    public void OnSliderUpdate(float value)
    {
        value = Mathf.Round(value * 100f) / 100f;
        GameSettings.SetGlobalVolume(value);
        text.SetText($"Game Volume: {value * 100f}%");
    }
}
