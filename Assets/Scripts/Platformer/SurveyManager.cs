using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SurveyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI surveyCounter;
	private int questionCounter = 1;
	private int questions = 5;
	public int coinCounter = 0;

	public void NextQuestion()
	{
		if (questionCounter < questions)
		{
			questionCounter++;
			surveyCounter.text = "Question " + questionCounter + "/" + questions;
		}
		else
		{
			PlayerPrefs.SetInt("totalCoins", PlayerPrefs.GetInt("totalCoins", 0) + coinCounter);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}
}