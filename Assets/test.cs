using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
	public TextMeshPro tester;
	public TouchControls tc;

	//Touch test (works)
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//tester.text = "bruh";
		Destroy(collision.gameObject);
	}

	//Pinch test
	private void Update()
	{
		tester.text = "orthographic size: " + tc.mainCamera.orthographicSize + "\nstartzoom: " + tc.startZoom + "\n f1 phase: " + tc.finger1.phase + "\n f2 phase: " + tc.finger2.phase;
	}
}