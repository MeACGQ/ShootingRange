using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    PlayerInv_Ui_Handler Inv_Ui_Handler;
    HighlightItem highlightItem;

    [SerializeField] private ItemData[] items = new ItemData[5];
    [SerializeField] private WeaponRunTimeState[] weaponStates = new WeaponRunTimeState[5];

    [SerializeField] private int[] slotStack = new int[5];

    [SerializeField] public GameObject holdingObject;

    int currentSlot;

    private void Start()
    {
        Inv_Ui_Handler = GetComponent<PlayerInv_Ui_Handler>();
        highlightItem = GetComponent<HighlightItem>();
    }

    public void UseItem()
    {
        if (items[currentSlot] == null)
            return;

        if (items[currentSlot].isUseble)
        {
            holdingObject.GetComponent<ItemBase>().UseItem();
        }
        else
            return;
    }

    public void ChangeSlot(int _currentSlot)
    {
        SaveCurrentWeaponState();

        currentSlot = _currentSlot;
        Inv_Ui_Handler.selectSlot(currentSlot);

        if (holdingObject != null)
            highlightItem.DestroyItem();

        if (items[currentSlot] == null) return;

        holdingObject = highlightItem.Highlight(items[currentSlot].ItemObject);

        if (weaponStates[currentSlot] != null)
        {
            GunBase gunScript = holdingObject.GetComponent<GunBase>();
            Magazine magazine = holdingObject.GetComponent<Magazine>();

            magazine.currentBullets = weaponStates[currentSlot].currentBullet;
            gunScript.totalBullets = weaponStates[currentSlot].totalBullet;
        }
    }

    public bool AddItemToInv(ItemData itemData)
    {
        bool itemAddSuccesful = false;

        if (items[currentSlot] == null)
        {
            items[currentSlot] = itemData;

            slotStack[currentSlot] += 1;

            if (itemData.ItemObject.CompareTag("Gun"))
            {
                weaponStates[currentSlot] = new WeaponRunTimeState();

                GunBase prefabGunScript = itemData.ItemObject.GetComponent<GunBase>();
                Magazine magazine = itemData.ItemObject.GetComponent<Magazine>();

                weaponStates[currentSlot].currentBullet = magazine.capacity;
                weaponStates[currentSlot].totalBullet = prefabGunScript.totalBullets;
            }
        }

        else if (items[currentSlot] == itemData && itemData.isStackable)
        {
            slotStack[currentSlot] += 1;
        }

        else
        {
            Debug.Log("Current slot is not empty");

            itemAddSuccesful = false;

            return itemAddSuccesful;
        }

        Inv_Ui_Handler.SelectItemImage(currentSlot, itemData);
        Inv_Ui_Handler.WriteStackCount(currentSlot, slotStack[currentSlot]);

        holdingObject = highlightItem.Highlight(items[currentSlot].ItemObject);

        itemAddSuccesful = true;

        return itemAddSuccesful;
    }

    public void DropItemFromInv(Vector3 dropPoint)
    {
        if (items[currentSlot] == null)
            return;

        if (items[currentSlot].isStackable)
        {
            Instantiate(items[currentSlot].ItemObject, dropPoint, Quaternion.identity);

            slotStack[currentSlot] -= 1;

            if (slotStack[currentSlot] == 0)
            {
                items[currentSlot] = null;

                Inv_Ui_Handler.RemoveItemImage(currentSlot);
            }
        }
        else
        {
            Instantiate(items[currentSlot].ItemObject, dropPoint, Quaternion.identity);

            items[currentSlot] = null;

            slotStack[currentSlot] -= 1;

            Inv_Ui_Handler.RemoveItemImage(currentSlot);

            highlightItem.DestroyItem();
        }

        Inv_Ui_Handler.WriteStackCount(currentSlot, slotStack[currentSlot]);
    }

    private void SaveCurrentWeaponState()
    {
        if (holdingObject != null && weaponStates[currentSlot] != null)
        {
            GunBase gunScript = holdingObject.GetComponent<GunBase>();
            Magazine magazine = holdingObject.GetComponent<Magazine>();

            weaponStates[currentSlot].currentBullet = magazine.capacity;
            weaponStates[currentSlot].totalBullet = gunScript.totalBullets;
        }
    }
}
