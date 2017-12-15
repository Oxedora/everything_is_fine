using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour {

    public Management_script manager;

    private Text text_People;
    private Text text_Budget;
    private Text text_Timer;

    // Use this for initialization
    void Awake () {
        text_People = GameObject.Find("SavedPeople_Recap").GetComponent<Text>();
        text_Budget = GameObject.Find("Budget_Recap").GetComponent<Text>();
        text_Timer = GameObject.Find("Timer_Recap").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetValues(int nbSafeAgent, int totalAgents, int initialBudget, int budget, float timeElapsed)
    {
        text_People.text = nbSafeAgent + " / " + totalAgents;
        text_Budget.text = initialBudget - budget + "€";

        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        text_Timer.text = niceTime;
    }
}
