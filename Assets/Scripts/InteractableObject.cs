using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableObject : MonoBehaviour, IInteractive
{
    [SerializeField] private string[] _itemRequirement;
    public string[] ItemRequirements { get { return _itemRequirement; } }
    [SerializeField] private UnityEvent InteractEvent;
    public abstract void Interacted();
}
