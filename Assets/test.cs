using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
	public TextMeshPro tester;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		tester.text = "bruh";
		Destroy(collision.gameObject);
	}
}