using UnityEngine;

public abstract class ItemBase : InteractbleBase
{
    [SerializeField] ItemData itemData;

    [SerializeField] GameObject ItemPrefab;
    [SerializeField] string ItemName;
    [SerializeField] int ID;

    private void Start()
    {
        gameObject.AddComponent<Rigidbody>();

        gameObject.layer = 6;

        ItemName = itemData.ItemName;
        ItemPrefab = itemData.ItemObject;
        ID = itemData.ItemID;
    }

    public override void Interact()
    {
        Debug.Log("Interact edildi : " + itemData.ItemName);

        PlayerInventory inv = FindFirstObjectByType<PlayerInventory>();

        if (itemData == null)
        {
            Debug.LogWarning("data is null");
        }
        else
        {
            bool added = inv.AddItemToInv(itemData);

            if (added)
                Destroy(gameObject);
        }
    }

    public virtual void UseItem()
    {
        Debug.Log("Used : " + gameObject.name);
    }
}
