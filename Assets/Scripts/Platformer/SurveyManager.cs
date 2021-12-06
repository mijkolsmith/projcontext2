using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SurveyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI surveyCounter;
	private int counter = 1;
	private int questions = 5;
	public int coinCounter;

	public void NextQuestion()
	{
		if (counter < questions)
		{
			counter++;
			surveyCounter.text = "Question " + counter + "/" + questions;
		}
		else
		{
			PlayerPrefs.SetInt("totalXP", PlayerPrefs.GetInt("totalXP", 0) + coinCounter);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}
}