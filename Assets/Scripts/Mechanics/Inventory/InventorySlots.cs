using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image slotImage;
    public ItemInstance itemInstance;

    public bool IsFull => itemInstance != null && itemInstance.itemData != null;

    private void Awake()
    {
        slotImage = GetComponent<Image>();

        // —бросить itemInstance, если в инспекторе был мусор
        if (itemInstance != null && itemInstance.itemData == null)
        {
            itemInstance = null;
        }
    }
    public void Clear() 
    {
        itemInstance = null;
        slotImage = null;
    }
}
