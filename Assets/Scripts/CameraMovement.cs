using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    [SerializeField] private GameObject Self;
    [SerializeField] private float speed;
    private void Start()
    {
        EventManager.Instance.Subscribe(EV.RecupObjet, MoveCameraToward);
    }

    private void MoveCameraToward(object[] args)
    {
        GameObject ClickedObject = (GameObject)args [0];
        Self.transform.position = Vector3.MoveTowards(Self.transform.position, ClickedObject.transform.position, speed);
    }

    private void Update()
    {
    }

}