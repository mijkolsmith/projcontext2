using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private Touch finger1, finger2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject hitObject;
    private float startZoom;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {
                if (finger1.phase == TouchPhase.Began || finger2.phase == TouchPhase.Began)
                {// Assign these 2 fingers to their own variables
                    finger1 = Input.GetTouch(0);
                    finger2 = Input.GetTouch(1);
                    startZoom = mainCamera.orthographicSize;
                }

                // When the fingers have moved, update the script to the new location
                if (finger1.phase == TouchPhase.Moved || finger2.phase == TouchPhase.Moved)
                {
                    float newDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                    float oldDistance = Vector2.Distance(finger1.position, finger2.position);
                    // Percentage to zoom in / zoom out
                    float zoomFactor = oldDistance / newDistance;
                    // Zoom in / zoom out
                    mainCamera.orthographicSize = startZoom * zoomFactor;
                }

                if (finger1.phase == TouchPhase.Ended || finger2.phase == TouchPhase.Ended)
				{
                    startZoom = mainCamera.orthographicSize;
                }
            }
            else
            {
                // Tap effect
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    // A tap is a quick touch and release
                    if (touch.phase == TouchPhase.Ended && touch.tapCount == 1)
                    {
                        // Touches are screen locations, this converts them in to world locations
                        Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                        // Create an empty gameObject at position with a trigger to trigger hitboxes
                        Instantiate(hitObject, position, Quaternion.identity, null);
                    }
                }
            }
        }
    }
}