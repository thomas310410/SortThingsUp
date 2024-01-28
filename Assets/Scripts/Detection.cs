using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
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
        Debug.Log(TriggeredObject.gameObject.name);
        if (ObjetADetecter == TriggeredObject.gameObject)
        {
            Debug.Log("coucou");
        }
    }
}


