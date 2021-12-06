using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class AdvanceSurvey : MonoBehaviour
{
    private TextMeshProUGUI surveyCounter;
	private int counter = 1;
	private int questions = 5;

	private void Start()
	{
		surveyCounter = GetComponent<TextMeshProUGUI>();
	}

	public void NextQuestion()
	{
		if (counter < questions)
		{
			counter++;
			surveyCounter.text = "Question " + counter + "/" + questions;
		}
		else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}
}