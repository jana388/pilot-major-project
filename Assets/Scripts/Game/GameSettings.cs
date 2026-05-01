using System;
using TMPro;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset defaultFont;
    public static TMP_FontAsset DefaultFont;
    [SerializeField] private TMP_FontAsset dyslexicFont;
    public static TMP_FontAsset DyslexicFont;


    public static float GameVolume = 1f;

    public static bool DoDyslexiaFont;
    public static bool DoPixelEffect;

    public static event Action OnSettingsUpdate;

    private void Awake()
    {
        DefaultFont = defaultFont;
        DyslexicFont = dyslexicFont;
        InitialiseSettings();
    }

    public static void SetGlobalVolume(float volume)
    {
        volume = Mathf.Round(volume * 100f) / 100f;
        volume = Mathf.Clamp01(volume);
        GameVolume = volume;
        AudioListener.volume = volume;

        PlayerPrefs.SetFloat("Volume_Global", GameVolume);
        PlayerPrefs.Save();

        OnSettingsUpdate?.Invoke();
    }

    public static void SetDyslexiaFont(bool state)
    {
        var value = state ? 1 : 0;
        DoDyslexiaFont = state;
        PlayerPrefs.SetInt("Settings_DyslexiaFont", value);
        PlayerPrefs.Save();

        OnSettingsUpdate?.Invoke();
    }

    public static void SetPixelEffect(bool state)
    {
        var value = state ? 1 : 0;
        DoPixelEffect = state;
        PlayerPrefs.SetInt("Settings_PixelEffect", value);
        PlayerPrefs.Save();

        OnSettingsUpdate?.Invoke();
    }


    private static void InitialiseSettings()
    {
        GameVolume = PlayerPrefs.GetFloat("Volume_Global");

        DoDyslexiaFont = PlayerPrefs.GetInt("Settings_DyslexiaFont") == 1;
        DoPixelEffect = PlayerPrefs.GetInt("Settings_PixelEffect") == 1;
    }
}
