using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public bool isUseble;
    public bool isStackable;

    public string ItemName;
    public int ItemID;
    public GameObject ItemObject;
    public Sprite ItemUiImage;
}