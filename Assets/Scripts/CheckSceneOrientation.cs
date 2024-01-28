using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static S_RotatorCam;

public class CheckSceneOrientation : MonoBehaviour
{
    private S_RotatorCam SceneOrientationController; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SceneOrientationController = FindObjectOfType<S_RotatorCam>();
        SceneOrientation currentOrientation = SceneOrientationController.currentSceneOrientation;
        if (currentOrientation == SceneOrientation.Ouest)
        {
            Debug.Log("CACHE");
            DeactivateSelf();
        }
        else
            ReactivateSelf();
    }

    private void ReactivateSelf()
    {
        gameObject.SetActive(true);
    }

    void DeactivateSelf() 
    {
        gameObject.SetActive(false);
    }   
}
