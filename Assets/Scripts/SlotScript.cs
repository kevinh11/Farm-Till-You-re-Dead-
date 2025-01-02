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
    private Text qtyText; 

    private SfxManager sfxManager;
    public Sprite active;
    public Sprite normal;
    public string slotItemName;
    private PlayerInventory inventoryManager;
    private InventoryUIManager UIManager;
    private Outline outline;

    public void UpdateSelected()
    {
        
        if (isSelected) 
        {
            outline.enabled = true;
            return;            
        }

        outline.enabled = false;
    }
    void Awake()
    {
        inventoryManager = GameObject.Find("Player").GetComponent<PlayerInventory>();
        sfxManager = GameObject.Find("SfxManager").GetComponent<SfxManager>();

    }

    bool GetSelected() 
    {
        return this.isSelected;
    }

    void ToggleSelected()
    {
        this.isSelected = !isSelected;
        UpdateSelected();
    }

    void Start()
    {
        slotButton = GetComponent<Button>();
        slotImage = transform.Find("Image").GetComponent<Image>();
        outline = slotImage.GetComponent<Outline>();
        // slotButton.onClick.AddListener(ToggleSelected);
        qtyText = transform.Find("Counter").GetComponent<Text>();
        DisplayItemCount();
    }

    void DisplayItemCount()
    {
        quantity = inventoryManager.GetItemQty(slotItemName); 
        qtyText.text = quantity.ToString(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("test bro");  
            if (!isSelected) {return;}
            if (inventoryManager.ConsumeItem(slotItemName)) {
                sfxManager.PlayCrunchSound();
            }

            else {
                sfxManager.PlayErrorSound();
            }
            
        }

        DisplayItemCount();
    }
}
