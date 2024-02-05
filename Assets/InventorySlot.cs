using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public bool isEmpty;
    public Image Icon;
    public void SetIsEmpty(bool isEmpty, Sprite sprite)
    {
        this.isEmpty = isEmpty;
        Icon.sprite = isEmpty ? null : sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
