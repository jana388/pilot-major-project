using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    public static Settings instance;
    public float outlineWidth = 10;

    private void OnEnable()
    {
        instance = this;
    }
}
