using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotScript : MonoBehaviour
{
    private Button slotButton;
    private bool isSelected = false;
    private Image slotImage;
    private int quantity;
    private TextMeshProUGUI qtyText; 

    public Sprite active;
    public Sprite normal;
    public string name;
    private PlayerInventory inventoryManager;

    void Start()
    {
        slotButton = GetComponent<Button>();
        slotImage = GetComponent<Image>();
        slotButton.onClick.AddListener(ToggleSelected);
        inventoryManager = GameObject.Find("Player").GetComponent<PlayerInventory>();


        qtyText = transform.Find("itemQty").GetComponent<TextMeshProUGUI>();
        DisplayItemCount();
    }

    void DisplayItemCount()
    {
        
        quantity = inventoryManager.GetItemQty(name); 
        qtyText.text = quantity.ToString(); 
    }

    void ToggleSelected()
    {
        isSelected = !isSelected;

        if (isSelected) {
            slotImage.sprite = active;
            return;
        }

        slotImage.sprite = normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
