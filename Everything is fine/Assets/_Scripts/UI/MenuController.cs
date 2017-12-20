using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    private AudioSource musique;

	// Use this for initialization
	void Start () {
        GameVariables.UserPrefToVariables();
        musique = GetComponent<AudioSource>();
        if(musique != null)
        {
            Debug.Log(GameVariables.volMusic);
            musique.volume = GameVariables.volMusic;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (musique != null)
        {
            musique.volume = GameVariables.volMusic;
        }
    }

	public void QuitApplication() {
		GameVariables.VariablesToUserPref();
		Application.Quit();
	}
}
