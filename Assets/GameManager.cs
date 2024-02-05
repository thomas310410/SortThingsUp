using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera camera;
    public inventory inventory;
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                Debug.Log(hit.collider.name);
            }

            if (hit.collider.tag == "ABouger")
            {
                Debug.Log("Peut bouger !");
                hit.collider.gameObject.SetActive(false);
                inventory.Put(hit.collider.gameObject.GetComponent<ClicOnMe>());
            }
            else
            {
                Debug.Log("Ne peut pas bouger.");
            }
        }
    }
}