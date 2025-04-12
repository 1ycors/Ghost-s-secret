using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image slotImage;

    public string itemName;
    public Sprite itemIcon;

    public bool isFull = false;

    private void Awake()
    {
        slotImage = GetComponent<Image>();
    }
}
