using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RotatorCam : MonoBehaviour
{
    public SceneOrientation currentSceneOrientation = SceneOrientation.Sud;
    [SerializeField]private Transform cameraTransform;
    [SerializeField] private Transform target; // Le point de pivot (le centre de la chambre)
    [SerializeField] private float distanceFromTarget = 2.0f; // Distance de la caméra par rapport au point de pivot
    private int currentViewIndex = 0;
    private Vector2 startTouchPosition;
    private bool isSwiping = false;
    private float swipeThreshold = 50f; // Distance in pixels for the swipe to be effective

    private float isoAngleX = 30.0f; // Angle de 30 degrés vers le bas par rapport à l'horizontale
    private float isoAngleY = 45.0f; // Angle isométrique standard pour la rotation Y

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool isTransitioning = false;
    [SerializeField] private float transitionSpeed = 3.0f; // Vitesse de la transition
    public enum SceneOrientation { Sud, Est, Nord, Ouest };
    public SceneOrientation CurrentsceneOrientation;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
        {
            // Interpolation smooth de la position et de la rotation pendant la transition
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, Time.deltaTime * transitionSpeed);
            cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, targetRotation, Time.deltaTime * transitionSpeed);

            // Arrêter la transition lorsque la caméra est suffisamment proche de la cible
            if (Vector3.Distance(cameraTransform.position, targetPosition) < 0.01f)
            {
                cameraTransform.position = targetPosition; // Pour être sûr que la caméra est exactement à la bonne position
                isTransitioning = false;
            }
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;

        }

        if (isSwiping && Input.GetMouseButton(0))
        {
            Vector2 currentTouchPosition = Input.mousePosition;
            Vector2 distance = currentTouchPosition - startTouchPosition;

            if (Mathf.Abs(distance.x) > swipeThreshold)
            {
                if (distance.x > 0)
                {
                    SwitchView(-1); // Swipe right
                    TurnSceneCounterclockwise();    
                }
                else
                {
                    SwitchView(1); // Swipe left
                    TurnSceneClockwise();   
                }
                isSwiping = false;
                isTransitioning = true; // Commencer la transition
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
    }

    private void SwitchView(int direction)
    {
        currentViewIndex += direction;

        if (currentViewIndex < 0)
            currentViewIndex = 3;
        else if (currentViewIndex > 3)
            currentViewIndex = 0;

        UpdateCameraPosition();
        isTransitioning = true; // Commencer la transition
    }

    private void UpdateCameraPosition()
    {
        // Calculez la nouvelle position de la caméra pour la vue isométrique
        Vector3 offset = Quaternion.Euler(isoAngleX, currentViewIndex * 90 + isoAngleY, 0) * new Vector3(0, 0, -distanceFromTarget);
        targetPosition = target.position + offset;
        targetRotation = Quaternion.LookRotation(target.position - targetPosition);
    }
    public void TurnSceneClockwise()
    {
        int currentOrientationIndex = (int)currentSceneOrientation;
        int nextOrientationIndex = (currentOrientationIndex + 1) % System.Enum.GetValues(typeof(SceneOrientation)).Length;

        currentSceneOrientation = (SceneOrientation)nextOrientationIndex;

        Debug.Log("Current Scene Orientation: " + currentSceneOrientation);

    }
    public void TurnSceneCounterclockwise()
    {
        int currentOrientationIndex = (int)currentSceneOrientation;
        int totalOrientations = System.Enum.GetValues(typeof(SceneOrientation)).Length;
        int nextOrientationIndex = (currentOrientationIndex - 1 + totalOrientations) % totalOrientations;

        currentSceneOrientation = (SceneOrientation)nextOrientationIndex;

        Debug.Log("Current Scene Orientation: " + currentSceneOrientation);
    }
}
