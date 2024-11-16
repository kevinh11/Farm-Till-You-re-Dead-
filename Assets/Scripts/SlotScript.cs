using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotScript : MonoBehaviour
{
    private Button slotButton;
    public bool isSelected = false;
    private Image slotImage;
    private int quantity;
    private TextMeshProUGUI qtyText; 

    public Sprite active;
    public Sprite normal;
    public string name;
    private PlayerInventory inventoryManager;

    private InventoryUIManager UIManager;


    public void UpdateSelected()
    {
        
        if (isSelected) 
        {
            slotImage.sprite = active;
            return;            
        }

        slotImage.sprite = normal;
    }
    void Awake()
    {
        inventoryManager = GameObject.Find("Player").GetComponent<PlayerInventory>();

    }


    bool GetSelected() 
    {
        return this.isSelected;
    }

    void setSelected()
    {
        this.isSelected = !isSelected;
    }

    void Start()
    {
        slotButton = GetComponent<Button>();
        slotImage = GetComponent<Image>();
        // slotButton.onClick.AddListener(ToggleSelected);

        qtyText = transform.Find("itemQty").GetComponent<TextMeshProUGUI>();
        DisplayItemCount();
    }

    void DisplayItemCount()
    {
        quantity = inventoryManager.GetItemQty(name); 
        qtyText.text = quantity.ToString(); 
    }

  

    // Update is called once per frame
    void Update()
    {
        DisplayItemCount();
    }
}
