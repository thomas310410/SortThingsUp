using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClicOnMe : MonoBehaviour
{
    public bool ClicObjet = false;
    public Sprite icon;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        Debug.Log("clic sur" + this.gameObject.name);
        ClicObjet = true;
    }
}
