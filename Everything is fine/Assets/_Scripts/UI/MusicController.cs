using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour {

	public Slider volM;

	// Use this for initialization
	void Start () {
		volM.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ValueChangeCheck() {
		GameVariables.volMusic = GameVariables.SliderValueToVolume(volM.value);
	}
}
