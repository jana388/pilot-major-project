using UnityEngine;
using UnityEngine.UI;

public class GameSettingsMenu : MonoBehaviour
{
    [SerializeField] private Toggle dyslexiaToggle, pixelationToggle;

    [SerializeField] private Material pixelationMaterial;



    private void Start()
    {
        dyslexiaToggle.isOn = GameSettings.DoDyslexiaFont;
        pixelationToggle.isOn = GameSettings.DoPixelEffect;
    }

    public void OnDyslexiaFontToggle(bool value)
    {
        GameSettings.SetDyslexiaFont(value);
    }

    public void DoPixelEffectToggle(bool value)
    {
        GameSettings.SetPixelEffect(value);
        pixelationMaterial.SetInt("_Pixelate", value? 1 : 0);
    }
}
