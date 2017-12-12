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

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("GameManager").GetComponent<Management_script>();
        buttonText = gameObject.GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        if(launched)
        {
            manager.agentsPos.RestartAgents();
            buttonText.text = launchText;
        }
        else
        {
            buttonText.text = stopText;
        }
        launched = !launched;
        manager.agentsPos.gameObject.SetActive(launched);
        manager.bottom.SetActive(!launched);
    }
}
