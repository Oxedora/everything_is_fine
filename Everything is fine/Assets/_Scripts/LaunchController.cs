using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaunchController : MonoBehaviour {

    private bool launched = false;
    private Text buttonText;
    private Management_script manager;

    private string launchText = "Launch !";
    private string stopText = "Abort";

    private float timeLeft;
    public float timeElapsed = 0f;
	// Use this for initialization
	void Awake () {
        manager = GameObject.Find("GameManager").GetComponent<Management_script>();
        buttonText = gameObject.GetComponentInChildren<Text>();
        timeLeft = manager.time;
    }
	
	// Update is called once per frame
	void Update () {
		if(launched)
        {
            if (manager.agentsPos.NbDeadAgent + manager.agentsPos.NbSafeAgent == manager.agentsPos.TotalAgents)
            {
                manager.endGame = true;
                manager.timeElapsed = timeElapsed;
                manager.EndGamePanel(manager.agentsPos.NbSafeAgent, manager.agentsPos.TotalAgents);
                SwitchMode();
            }
            else
            {
                timeElapsed += Time.deltaTime;
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0f)
                {
                    timeLeft = 0f;
                    manager.text_time.color = Color.red;
                }
                int minutes = Mathf.FloorToInt(timeLeft / 60F);
                int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
                manager.text_time.text = niceTime;
            }
        }
	}

    public void OnClick()
    {
        SwitchMode();
    }

    private void SwitchMode()
    {
        if (launched)
        {
            timeLeft = manager.time;
            manager.text_time.color = Color.black;
            manager.agentsPos.RestartAgents();
            manager.fireSourceMan.ResetFireSources();
            buttonText.text = launchText;
        }
        else
        {
            timeElapsed = 0f;
            buttonText.text = stopText;
        }
        launched = !launched;
        manager.agentsPos.gameObject.SetActive(launched);
        manager.fireSourceMan.gameObject.SetActive(launched);
        manager.bottom.SetActive(!launched);
        manager.Node.SetActive(launched);
        manager.playInfo.SetActive(launched);
    }
}
