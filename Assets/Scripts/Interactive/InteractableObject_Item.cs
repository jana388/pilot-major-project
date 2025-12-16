using UnityEngine;

public class InteractableObject_Item : InteractableObject
{
    [Header("Interactable Item")]
    [SerializeField] string _itemName;
    public string ItemName { get { return _itemName; } }

    public override void Interacted()
    {
        base.Interacted();

        gameObject.SetActive(false);
    }
}
