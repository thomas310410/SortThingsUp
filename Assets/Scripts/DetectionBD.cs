using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBD : MonoBehaviour
{
    [Header("need GameObject with rigidbody and collider")]
    [SerializeField] public GameObject ObjetADetecter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider TriggeredObject)
    {
        
        if (ObjetADetecter == TriggeredObject.gameObject)
        {
           /* Debug.Log(TriggeredObject.gameObject.name);*/
            Debug.Log("La brosse a dents est a sa place!");
        }
    }
}