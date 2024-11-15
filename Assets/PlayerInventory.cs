 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    struct inventoryItem 
    {
        public string itemName;
        public float itemHeal;

        public inventoryItem(string name, float heal)
        {
            itemName = name;
            itemHeal = heal;
        }
    }

    private PlayerInteract interactScript;
    private List<inventoryItem> cropsList = new List<inventoryItem>();

    public int GetItemQty(string name) 
    {
        // int quantity = cropsList.Where(
        //     crop => crop.name == this.name
        // ).Length;

        int quantity = 0;
        foreach (var crop in cropsList)
        {
            if (crop.itemName == name) quantity++;
        }

        return quantity ;
    }
    
    void Start()
    {
        AddToInventory("Jagung", 30f);
        AddToInventory("Jagung", 30f);

        interactScript = GetComponent<PlayerInteract>();

        
    }

   


    void ConsumeItem(string itemName)
    {
        inventoryItem selectedItem = cropsList.Find(item => item.itemName == itemName);
        if (selectedItem.Equals(default(inventoryItem))) 
        {
            Debug.Log("Crop " + itemName + " sedang kosong!");
            return;
        }

        interactScript.AddHealth(selectedItem.itemHeal);  
        Debug.Log("Player telah memakan 1 buah" + itemName); 
        cropsList.Remove(selectedItem);
    }
    

    void AddToInventory(string itemName, float itemHeal)
    {
        inventoryItem newItem = new inventoryItem(itemName, itemHeal);
        cropsList.Add(newItem);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ConsumeItem("Jagung");
        }
    }
}
