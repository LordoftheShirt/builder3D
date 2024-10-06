using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public GameObject spawnCube;
    public Vector3 screenPosition;
    public Vector3 worldPosition;
    [SerializeField] private Material findMaterial;
    Plane plane = new Plane(Vector3.down, 0);
    static public bool click;
    private bool holdClick;
    [SerializeField] private Color opaque, originalColour;

    // 2: Thése variables pair with method 2. Not in use.
    public LayerMask layerToHit;
    Ray ray;



    void Update()
    {
        screenPosition = Input.mousePosition;
        click = Input.GetMouseButtonDown(0);
        holdClick = Input.GetMouseButton(0);
        CursorLocationRecord();

        MouseButtonBeingHeld();

    }
    private void CursorLocationRecord()
    {
        // 1: Using this method makes it so that it interprets your mouse moving only on a 2D plane 1 unit away from your where the camera's depth begins.
        //screenPosition.z = Camera.main.nearClipPlane + 1;
        //worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        ray = Camera.main.ScreenPointToRay(screenPosition);

        // 2: Sends a ray from Camera's NearClipPlane out into its cone. Records value wherever this ray collides with an object. Layermasks help ignore unwanted collisions with object X.
        /* 
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerToHit))
        {
            worldPosition = hitInfo.point;
        }
        */

        if (plane.Raycast(ray, out float distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        transform.position = worldPosition;
    }

    private void MouseButtonBeingHeld()
    {
        if (holdClick)
        {
            findMaterial.color = opaque;
        }
        else
        {
            findMaterial.color = originalColour;
        }
    }
}

