using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    
    public struct inventoryItem
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
    public List<inventoryItem> cropsList = new List<inventoryItem>();


    public int GetItemQty(string name)
    {
        int quantity = 0;
        foreach (var crop in cropsList)
        {
         
            if (crop.itemName == name) quantity++;
        }

        return quantity;
    }

    void Start()
    {
        interactScript = GetComponent<PlayerInteract>();
    }

    void PrintCropsList()
    {
        Debug.Log("Crops List:");
        for (int i = 0; i < cropsList.Count; i++)
        {
            Debug.Log($"Index {i}: Item: {cropsList[i].itemName}, Heal: {cropsList[i].itemHeal}");
        }
    }
    int FindIdxByName(string itemName)
    {  
        for (int i = 0; i < cropsList.Count; i++)
        {
            if (cropsList[i].itemName.Trim() == itemName.Trim()) return i;
        }

        return -1;
    }

    public bool ConsumeItem(string itemName)
    {

        PrintCropsList();
        int index = FindIdxByName(itemName);
        Debug.Log("index of entry" + itemName+":"+index);
        if (index == -1) return false;
        inventoryItem selectedItem = cropsList[index];
        interactScript.AddHealth(selectedItem.itemHeal);
        cropsList.RemoveAt(index);


        return true;

    }

    



    public void AddToInventory(string itemName, float itemHeal)
    {
        inventoryItem newItem = new inventoryItem(itemName, itemHeal);
        cropsList.Add(newItem);

        
    }

    void Update()
    {
      
       
    }
}
