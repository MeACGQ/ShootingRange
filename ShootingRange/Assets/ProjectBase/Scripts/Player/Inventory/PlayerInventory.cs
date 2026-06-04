using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    PlayerInputAction actions;
    PlayerInv_Ui_Handler Inv_Ui_Handler;

    [SerializeField] private ItemData[] items = new ItemData[5];
    [SerializeField] private int[] slotStack = new int[5];

    int currentSlot;
    [SerializeField] GameObject holdReferance;

    GameObject holdingObject;

    private void Awake()
    {
        actions = new PlayerInputAction();
    }

    private void Start()
    {
        Inv_Ui_Handler = GetComponent<PlayerInv_Ui_Handler>();
    }

    private void OnEnable()
    {
        actions.Enable();

        actions.PlayerMoves.Fire.performed += ctx =>
        {
            if (items[currentSlot] == null)
                return;

            if (items[currentSlot].isUseble)
            {
                holdingObject.GetComponent<ItemBase>().UseItem();
            }
            else
                return;
        };

        actions.PlayerMoves.Reload.performed += ctx =>
        {
            if (holdingObject.CompareTag("Gun"))
            {
                holdingObject.GetComponent<GunBase>().Reload();
            }
            else
                Debug.Log("yok.");
        };

        actions.PlayerMoves.Slots.performed += ctx =>
        {
            currentSlot = int.Parse(ctx.control.name) - 1;

            Inv_Ui_Handler.selectSlot(currentSlot);

            HighlightItem(currentSlot);
        };
    }

    private void HighlightItem(int slotNumber)
    {
        if (holdingObject != null)
        {
            Destroy(holdingObject);
        }

        if (items[slotNumber] != null)
        {
            holdingObject = Instantiate(items[slotNumber].ItemObject,
                holdReferance.transform.position,
                holdReferance.transform.rotation,
                holdReferance.transform);
            
            holdingObject.GetComponent<Rigidbody>().useGravity = false;
            holdingObject.GetComponent<Collider>().enabled = false;
        }
    }

    public bool AddItemToInv(ItemData itemData)
    {
        bool itemAddSuccesful = false;

        if (items[currentSlot] == null)
        {
            items[currentSlot] = itemData;

            slotStack[currentSlot] += 1;
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

        HighlightItem(currentSlot);

        itemAddSuccesful = true;

        return itemAddSuccesful;
    }

    public PlayerInv_Ui_Handler GetInv_Ui_Handler()
    {
        return Inv_Ui_Handler;
    }

    public void DropItemFromInv(Vector3 dropPoint, PlayerInv_Ui_Handler ınv_Ui_Handler)
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
        }

        Inv_Ui_Handler.WriteStackCount(currentSlot, slotStack[currentSlot]);
        HighlightItem(currentSlot);
    }


}
