using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public List<InventorySlot> slots;

    internal void Put(ClicOnMe clicOnMe)
    {
        slots[0].SetIsEmpty(false, clicOnMe.icon);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (InventorySlot slot in slots)
        {
            slot.SetIsEmpty(true, null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
