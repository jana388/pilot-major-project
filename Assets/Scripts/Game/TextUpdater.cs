using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameSettings.OnSettingsUpdate += UpdateFont;
        UpdateFont();
    }
    private void OnDisable()
    {
        GameSettings.OnSettingsUpdate -= UpdateFont;
    }

    private void UpdateFont()
    {
        textMeshPro.font = GameSettings.DoDyslexiaFont ? GameSettings.DyslexicFont : GameSettings.DefaultFont;
    }
}
