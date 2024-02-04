using Hypertonic.GridPlacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAME_MANAGER : MonoBehaviour
{
    [SerializeField]
    private GridSettings gridSettings;

    [SerializeField]
    private GameObject gridObjectPrefab;

    private GridManager gridManager;

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 50), "Comfirm Placement"))
            ConfirmPlacement();

        if (GUI.Button(new Rect(10, 70, 150, 50), "Enter Placement Mode"))
            EnterPlacementMode();

    }
    private void start()
    {
        gridManager = new GameObject("Grid Manager").AddComponent<GridManager>();
        gridManager.Setup(gridSettings);
    }

    private void EnterPlacementMode()
    {
        GameObject gridObject = Instantiate(gridObjectPrefab);
        gridManager.EnterPlacementMode(gridObject);
    }

    private void ConfirmPlacement()
    {
        gridManager.ConfirmPlacement();
    }
}
