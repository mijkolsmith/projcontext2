using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    public Touch finger1, finger2; //public for testing
    [SerializeField] public Camera mainCamera; //public for testing
    [SerializeField] private GameObject hitObject;
    public float startZoom; //public for testing

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {
                // Assign these 2 fingers to their own variables
                if (finger1.phase == TouchPhase.Began || finger2.phase == TouchPhase.Began || (finger1.phase == TouchPhase.Stationary && finger2.phase == TouchPhase.Stationary))
                {
                    startZoom = mainCamera.orthographicSize;
                    finger1 = Input.GetTouch(0);
                    finger2 = Input.GetTouch(1);
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
        else if (finger1.phase != TouchPhase.Began || finger2.phase != TouchPhase.Began)
        {
            finger1.phase = TouchPhase.Began;
            finger2.phase = TouchPhase.Began;
        }
    }
}