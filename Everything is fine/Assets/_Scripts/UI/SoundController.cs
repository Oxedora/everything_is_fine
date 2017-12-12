using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

	public Slider volS;

	// Use this for initialization
	void Start () {
		volS.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ValueChangeCheck() {
		GameVariables.volSound = GameVariables.SliderValueToVolume(volS.value);
	}
}
