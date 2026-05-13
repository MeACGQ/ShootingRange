using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInv_Ui_Handler : MonoBehaviour
{
    [SerializeField] private Image[] slot = new Image[5];
    [SerializeField] private Image[] ItemImage = new Image[5];
    [SerializeField] private TextMeshProUGUI[] stack_TMPs = new TextMeshProUGUI[5];

    [SerializeField] private Sprite DefaultSlotImage;

    private void Start()
    {
        selectSlot(0);
    }

    private int currentSlot = 0;

    public void selectSlot(int slotNumber)
    {
        slot[currentSlot].color = Color.white;

        currentSlot = slotNumber;

        slot[currentSlot].color = Color.grey;

    }

    public void SelectItemImage(int slotNumber, ItemData itemData)
    {
        ItemImage[slotNumber].sprite = itemData.ItemUiImage;
    }

    public void RemoveItemImage(int slotNumber)
    {
        ItemImage[slotNumber].sprite = DefaultSlotImage;
    }

    public void WriteStackCount(int slotNumber, int stackCount)
    {
        stack_TMPs[slotNumber].text = stackCount.ToString();
    }
}
