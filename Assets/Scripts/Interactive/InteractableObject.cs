using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractive
{

    [SerializeField] private string interactionPrompt = "Interact";
    public string InteractionPrompt => interactionPrompt;
    [SerializeField] private string[] _itemRequirement;
    public string[] ItemRequirements { get { return _itemRequirement; } }
    [SerializeField] private UnityEvent _interactEvent;

    [SerializeField] private string keyboardPrompt;
    [SerializeField] private string gamepadPrompt;
     public string KeyboardPrompt => keyboardPrompt;
     public string GamepadPrompt => gamepadPrompt;
    [SerializeField] private GameContext context;

    public virtual void Interacted()
    {
        _interactEvent.Invoke();
        print("Just interacted");
    }

    public virtual void Interacted(string[] items)
    {
        // Check if the player has the items this object is required to be interacted with

        int check = 0;
        int requirement = _itemRequirement.Length;

        foreach (var item in items)
        {
            if (_itemRequirement.Contains(item))
                check++;
        }

        if (check == requirement)
        {
            _interactEvent.Invoke();
        }

        else
        {
            // need more items to interact
          
        }

        print("Requested to look for items");
    }

    public ItemRequirement CheckItemRequirement()
    {
        int count = _itemRequirement.Length;
        if (count > 0)
        {
            return ItemRequirement.ItemRequired;
        }
        
        return ItemRequirement.NoItem;
    }
}

public enum ItemRequirement { NoItem, ItemRequired }
