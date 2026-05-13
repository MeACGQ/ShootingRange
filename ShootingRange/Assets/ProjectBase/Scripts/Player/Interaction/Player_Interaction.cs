using UnityEngine;

public class Player_Interaction : MonoBehaviour
{
    PlayerInputAction action;
    PlayerInventory inventory;

    [Header("Raycast")]
    RaycastHit hit;
    Ray ray;
    [SerializeField] float rayDistance;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] bool seeRay = false;
    [SerializeField] bool seeOutline = true;

    IInteractble currentOutline;
    IInteractble lastOutline;

    private void Start()
    {
        inventory = GetComponentInParent<PlayerInventory>();
    }

    private void Awake()
    {
        action = new PlayerInputAction();

        currentOutline = null;
    }

    private void OnEnable()
    {
        action.Enable();

        action.PlayerMoves.Interactions.performed += ctx => interact();

        action.PlayerMoves.DropItem.performed += ctx => DropItem();
    }

    private void FixedUpdate()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (seeRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        }

        if (seeOutline)
        {
            if (Physics.Raycast(ray, out hit, rayDistance, rayLayer))
            {
                hit.collider.TryGetComponent<IInteractble>(out currentOutline);
            }
            else
            {
                if (currentOutline != null)
                {
                    currentOutline.GlowObject(false);

                    currentOutline = null;
                }
            }

            if (currentOutline != lastOutline)
            {
                if (lastOutline != null)
                    lastOutline.GlowObject(false);

                lastOutline = currentOutline;

                if (lastOutline != null)
                    lastOutline.GlowObject(true);
            }
        }
    }

    void interact()
    {
        if (Physics.Raycast(ray, out hit, rayDistance, rayLayer))
        {
            if (hit.collider.TryGetComponent<IInteractble>(out IInteractble interactObj))
            {
                interactObj.Interact();

                Debug.Log("Interacted with : " + interactObj);
            }
        }
    }

    void DropItem()
    {
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Vector3 dropPos = hit.point;

            inventory.DropItemFromInv(dropPos, inventory.GetInv_Ui_Handler());
        }
        else
        {
            Vector3 dropPos = ray.origin + ray.direction * rayDistance;

            inventory.DropItemFromInv(dropPos, inventory.GetInv_Ui_Handler());
        }
    }
}
