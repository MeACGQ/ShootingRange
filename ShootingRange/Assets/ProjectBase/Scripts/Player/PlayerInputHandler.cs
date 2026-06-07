using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInputAction actions;
    PlayerInventory inventory;

    private void Awake()
    {
        actions = new PlayerInputAction();
        inventory = GetComponent<PlayerInventory>();
    }

    private void OnEnable()
    {
        actions.Enable();

        actions.PlayerMoves.Fire.performed += ctx =>
        {
            inventory.UseItem();
        };

        actions.PlayerMoves.Reload.performed += ctx =>
        {
            if (inventory.holdingObject.CompareTag("Gun"))
            {
                inventory.holdingObject.GetComponent<GunBase>().Reload();
            }
            else
                Debug.Log("yok.");
        };

        actions.PlayerMoves.Slots.performed += ctx =>
        {
            int currentSlot = int.Parse(ctx.control.name) - 1;

            inventory.ChangeSlot(currentSlot);
        };
    }
}
