using UnityEngine;

public class HighlightItem : MonoBehaviour
{
    [SerializeField] GameObject holdReferance;
    GameObject holdingItem;

    public GameObject Highlight(GameObject item)
    {
        holdingItem = Instantiate(item,
            holdReferance.transform.position,
            holdReferance.transform.rotation,
            holdReferance.transform);

        holdingItem.GetComponent<Rigidbody>().useGravity = false;
        holdingItem.GetComponent<Collider>().enabled = false;

        return holdingItem;
    }

    public void DestroyItem()
    {
        Destroy(holdingItem);
    }
}