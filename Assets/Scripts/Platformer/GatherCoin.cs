using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatherCoin : MonoBehaviour
{
	[SerializeField] private SurveyManager surveyManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.name.Contains("Coin"))
		{
			Destroy(collision.gameObject);
			surveyManager.coinCounter++;
		}
	}
}