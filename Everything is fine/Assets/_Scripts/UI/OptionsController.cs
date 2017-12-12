using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {
	private GameObject volM;
	private GameObject volS;
	private GameObject help;
	// Use this for initialization
	void Start () {
		GameVariables.UserPrefToVariables();

		volM = GameObject.Find("Slider_musique");
		volM.GetComponent<Slider>().value = GameVariables.VolumeToSliderValue(GameVariables.volMusic);

		volS = GameObject.Find("Slider_son");
		volS.GetComponent<Slider>().value = GameVariables.VolumeToSliderValue(GameVariables.volSound);

		help = GameObject.Find("Toggle_aide");
		help.GetComponent<Toggle>().isOn = GameVariables.help;
	}
	
	// Update is called once per frame
	void Update () {
		GameVariables.VariablesToUserPref(); // à supprimer sur l'exe
	}
}
