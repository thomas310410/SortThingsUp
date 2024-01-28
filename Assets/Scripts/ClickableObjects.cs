using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        EventManager.Instance.Trigger(EV.RecupObjet, this.gameObject);
    }
}
