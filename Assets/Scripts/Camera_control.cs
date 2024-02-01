using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public float sensitivity = 100f; // Sensibilité de la rotation
    private float xRotation = 0f;
    private Vector2 lastTouchPosition;
    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Vector2 touchDelta = (touch.position - lastTouchPosition) * sensitivity * Time.deltaTime;

                // Calcul de la rotation autour de l'axe Y
                float yRotation = touchDelta.x;

                // Calcul de la rotation autour de l'axe X
                xRotation -= touchDelta.y;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                // Appliquer la rotation
                transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y + yRotation, 0);

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}
