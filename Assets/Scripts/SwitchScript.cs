using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour
{
	private new Camera camera;
	[SerializeField] private Camera otherCamera;
	private float pos1, pos2;
	private bool switching = false;
	private float timeStartedLerping;
	private float lerpTime = .25f;
	[SerializeField] private Player player;
	[SerializeField] private Button surveyButton;

	private void Start()
	{
		camera = GetComponent<Camera>();
	}

	private void Update()
	{
		if (switching)
		{
			float timeSinceStarted = Time.time - timeStartedLerping;
			float percentageComplete = timeSinceStarted / lerpTime;
			camera.rect = new Rect(0, Mathf.SmoothStep(pos1, pos2, percentageComplete), 1, 0.4f);
			otherCamera.rect = new Rect(0, Mathf.SmoothStep(pos2, pos1, percentageComplete), 1, 0.4f);

			if (pos1 > pos2)
			{
				if (camera.rect.y <= pos2 + 0.01f && otherCamera.rect.y >= pos1 - 0.01f)
				{
					camera.rect = new Rect(0, pos2, 1, 0.4f);
					otherCamera.rect = new Rect(0, pos1, 1, 0.4f);
					switching = false;
					surveyButton.interactable = true;
				}
			}
			else
			{
				if (camera.rect.y >= pos2 - 0.01f && otherCamera.rect.y <= pos1 + 0.01f)
				{
					camera.rect = new Rect(0, pos2, 1, 0.4f);
					otherCamera.rect = new Rect(0, pos1, 1, 0.4f);
					switching = false;
					StartCoroutine(DelayedJumpAndSwitchPosition());
				}
			}
		}
	}

	public void SwitchPosition()
	{
		if (switching == false)
		{
			pos1 = camera.rect.y;
			pos2 = otherCamera.rect.y;
			timeStartedLerping = Time.time;
			switching = true;
		}
	}

	IEnumerator DelayedJumpAndSwitchPosition()
	{
		yield return new WaitForSeconds(.05f);
		player.Jump();
		yield return new WaitForSeconds(2f);
		SwitchPosition();
	}
}