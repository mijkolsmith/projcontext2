using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScript : MonoBehaviour
{
	private new Camera camera;
	[SerializeField] private Camera otherCamera;
	[SerializeField] private GameObject cameraChains;
	[SerializeField] private GameObject otherCameraChains;
	private float pos1, pos2;
	private Vector3 size1 = Vector3.one, size2 = 1.7f * Vector3.one;
	private bool buttonResizing = false;
	private bool switching = false;
	private float timeStartedLerping;
	private float lerpTime = .4f;
	private float buttonTimer = 0f;
	private float buttonMaxSizeTime = 0.1f;
    [SerializeField] private Player player;
	[SerializeField] private Button surveyButton;
	[SerializeField] private Slider surveySlider;

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
			cameraChains.transform.localPosition = new Vector3(cameraChains.transform.localPosition.x, (2500f * camera.rect.y) - 528.21f, cameraChains.transform.localPosition.z);
			otherCameraChains.transform.localPosition = new Vector3(otherCameraChains.transform.localPosition.x, (2500f * otherCamera.rect.y) + 310f, otherCameraChains.transform.localPosition.z);

			if (buttonResizing) surveyButton.transform.localScale = Vector3.Lerp(size2, size1, percentageComplete);

			if (pos1 > pos2)
			{
				if (camera.rect.y <= pos2 + 0.01f && otherCamera.rect.y >= pos1 - 0.01f)
				{
					Reset();
					surveyButton.interactable = true;
					surveySlider.interactable = true;
				}
			}
			else
			{
				if (camera.rect.y >= pos2 - 0.01f && otherCamera.rect.y <= pos1 + 0.01f)
				{
					Reset();
					StartCoroutine(DelayedJumpAndSwitchPosition());
				}
			}
		}
	}

	public void Reset()
	{
		camera.rect = new Rect(0, pos2, 1, 0.4f);
		otherCamera.rect = new Rect(0, pos1, 1, 0.4f);
		switching = false;
		buttonResizing = false;
		buttonResizing = false;
        buttonTimer = 0f;

        if (surveyButton.transform.localScale.x <= (size1 * 1.1f).x)
		{
			surveyButton.transform.localScale = size1;
		}
		else if (surveyButton.transform.localScale.x >= (size2 * 0.9f).x)
		{
			surveyButton.transform.localScale = size2;
		}
	}

	private IEnumerator ResizeButtonThenSwitch()
	{
		while (buttonTimer < buttonMaxSizeTime)
		{
			buttonTimer += Time.deltaTime;
            surveyButton.transform.localScale = Vector3.Lerp(size1, size2, buttonTimer / buttonMaxSizeTime);
			yield return null;
        }
		buttonResizing = false;
        surveyButton.transform.localScale = size2;
        StartSwitching();
    }

	private void StartSwitching()
	{
		switching = true;
        pos1 = camera.rect.y;
        pos2 = otherCamera.rect.y;
        timeStartedLerping = Time.time;
    }


	public void SwitchPosition()
	{
		if (!buttonResizing)
		{
			buttonTimer = 0f;
			StartCoroutine(ResizeButtonThenSwitch());
            buttonResizing = true;
		}
	}

	IEnumerator DelayedJumpAndSwitchPosition()
	{
		player.Jump();
		yield return new WaitForSeconds(2f);
        buttonResizing = true;
        StartSwitching();

    }
}