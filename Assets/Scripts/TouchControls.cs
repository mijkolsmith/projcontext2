using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour
{
    private Touch finger1, finger2;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            // Assign these 2 fingers to their own variables
            finger1 = Input.GetTouch(0);
            finger2 = Input.GetTouch(1);

            // When the fingers have moved, update the script to the new location
            if (finger1.phase == TouchPhase.Moved || finger2.phase == TouchPhase.Moved)
            {
                float baseDistance = Vector2.Distance(this.finger1.position, this.finger2.position);
                float currentDistance = Vector2.Distance(finger1.position, finger2.position);
                // Percentage to zoom in / zoom out
                float currentDistancePercent = currentDistance / baseDistance;
                // Zoom in / zoom out
                // transform.localScale = Vector3.one * (currentDistancePercent * 1.5f);
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
                    // Create an empty gameObject at position with a trigger to trigger buttons or hitboxes
                }
            }
        }
    }
}