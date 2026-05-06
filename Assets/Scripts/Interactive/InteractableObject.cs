using System;
using System.Linq;
using Unity.VisualScripting;
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

    //[SerializeField] private Material outlineMaterial;
    private Outline outline;
    private void Start()
    {
        outline = gameObject.GetOrAddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = Settings.instance.outlineWidth;
        // Setup separate outline material
        //outlineMaterial = new Material(outlineMaterial);

        // Assign the new outline material to this mesh renderer
        //if (!TryGetComponent<MeshRenderer>(out var meshRenderer)) meshRenderer = GetComponentInChildren<MeshRenderer>();
        //var mats = meshRenderer.materials.ToList();
        //mats.Add(outlineMaterial);
        //meshRenderer.SetMaterials(mats);
    }

    public virtual void Interacted()
    {
        _interactEvent?.Invoke();
        gameObject.SetActive(false);
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
            print("Collected items");
            _interactEvent.Invoke();
        }

        else
        {
            // need more items to interact
          
        }

        //print("Requested to look for items");
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

    //private const string outlineStringID = "_EnableOutline";
    public void SetOutline(bool state)
    {
        //outlineMaterial.SetInt(outlineStringID, state? 1 : 0); // Set outline shader state
        outline.enabled = state;
    }
}

public enum ItemRequirement { NoItem, ItemRequired }
