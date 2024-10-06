using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBuilding : MonoBehaviour
{
    [SerializeField] private GameObject cursorObject;
    private MousePosition setBuildingOnCursor;
    static public bool spawnCubeBeenPressed = false;
    private bool locationSelected = false;
    private bool rotateBuildingPhase = false;
    private GameObject selectedBuilding, placedBuilding;
    private float gatherXData;
    private void Start()
    {
        setBuildingOnCursor = cursorObject.GetComponent<MousePosition>();
    }

    private void Update()
    {
        cubePlacementInteraction();
    }
    public void SpawnCubeClick()
    {
        if (spawnCubeBeenPressed == false)
        {
            Debug.Log("Button press!");
            selectedBuilding = Instantiate(setBuildingOnCursor.spawnCube);
        }
        else
        {
            Debug.Log("A building is already selected!");
        }
        spawnCubeBeenPressed = true;
    }

    public void cubePlacementInteraction()
    {
        if (spawnCubeBeenPressed == true)
        {
            if (locationSelected == false)
            {
                selectedBuilding.transform.position = cursorObject.transform.position;
                selectedBuilding.transform.Rotate(0, 0.4f, 0);
                if (MousePosition.click)
                {
                    Debug.Log("Location Selected!");
                    locationSelected = true;
                }
            }
            if (locationSelected == true)
            {
                gatherXData = setBuildingOnCursor.screenPosition.x;
                selectedBuilding.transform.rotation = Quaternion.Euler(0, -gatherXData, 0);
                if (rotateBuildingPhase == true)
                {
                    if (MousePosition.click)
                    {
                        Debug.Log("Building placed!");
                        placedBuilding = Instantiate(selectedBuilding);
                        Destroy(selectedBuilding);
                        spawnCubeBeenPressed = false;
                        locationSelected = false;
                        rotateBuildingPhase = false;
                    }
                }
                else
                {
                    Debug.Log("ROTATE BUILDING NOW");
                    rotateBuildingPhase = true;
                }
            }
        }
    }
}
