using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour {

	public Toggle helpController;

	// Use this for initialization
	void Start () {
		helpController.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ValueChangeCheck() {
		GameVariables.help = helpController.isOn;
	}
}
