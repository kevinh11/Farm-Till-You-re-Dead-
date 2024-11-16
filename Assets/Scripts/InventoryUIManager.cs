using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    private SlotScript slotOne;
    private SlotScript slotTwo;
    private SlotScript slotThree;

    private Button slotOneBtn;
    private Button slotTwoBtn;
    private Button slotThreeBtn;

    SlotScript[] slots;

    void Awake()
    {
        InitializeSlots();
        AddBtnListeners();
    }

    void InitializeSlots()
    {
        slotOne = transform.Find("SlotOne").GetComponent<SlotScript>();
        slotTwo = transform.Find("SlotTwo").GetComponent<SlotScript>();
        slotThree = transform.Find("SlotThree").GetComponent<SlotScript>();

        slots = new SlotScript[] { slotOne, slotTwo, slotThree }; 

        slotOneBtn = slotOne.GetComponent<Button>();
        slotTwoBtn = slotTwo.GetComponent<Button>();
        slotThreeBtn = slotThree.GetComponent<Button>();
    }

    void AddBtnListeners()
    {
        slotOneBtn.onClick.AddListener(() => ToggleActiveSlots("SlotOne"));
        slotTwoBtn.onClick.AddListener(() => ToggleActiveSlots("SlotTwo"));
        slotThreeBtn.onClick.AddListener(() => ToggleActiveSlots("SlotThree"));
    }

    void ToggleActiveSlots(string currentSlot)
    {
        switch (currentSlot)
        {
            case "SlotOne":
                slotOne.isSelected = true;
                slotTwo.isSelected = false;
                slotThree.isSelected = false;
                break;

            case "SlotTwo":
                slotOne.isSelected = false;
                slotTwo.isSelected = true;
                slotThree.isSelected = false;
                break;

            default:
                slotOne.isSelected = false;
                slotTwo.isSelected = false;
                slotThree.isSelected = true;
                break;
        }

        foreach (SlotScript slot in slots)
        {
            slot.UpdateSelected();
        }
    }
}
