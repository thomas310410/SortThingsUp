using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Adjust the speed as needed
    private Camera mainCamera;
    private Vector3 initialCameraPosition;
    private Vector3 targetPosition; // Position clicked by the player
    private InputManager inputManager;
    void Start()
    {
        mainCamera = Camera.main;
        initialCameraPosition = mainCamera.transform.position;
    }

    void Update()
    {
        // Check if the player interacts with an object (e.g., clicks)
        if (Input.GetMouseButtonDown(0))  // Change this to your input method (e.g., touch)
        {
            // Get the position clicked by the player from your input manager
            inputManager = FindObjectOfType<InputManager>();
            targetPosition = inputManager.GetSelectedMapPosition();

            // Raycast from the clicked position
            Ray ray = mainCamera.ScreenPointToRay(targetPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object has a specific tag or layer (customize as needed)
                if (hit.collider.CompareTag("Interactable"))
                {
                    Debug.Log("Clicked");
                    MoveCameraToTarget(hit.transform);
                }
            }
        }
    }

    void MoveCameraToTarget(Transform target)
    {
        // Calculate the new position for the camera based on the target object
        Vector3 newCameraPosition = target.position;
        newCameraPosition.y = mainCamera.transform.position.y;  // Maintain the same height

        // Move the camera towards the target position smoothly
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCameraPosition, moveSpeed * Time.deltaTime);
    }

    // Call this method to reset the camera position to its initial state
    public void ResetCameraPosition()
    {
        mainCamera.transform.position = initialCameraPosition;
    }
}