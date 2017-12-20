using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Object_Script : MonoBehaviour {

	public GameObject prefab;
    private AudioSource sound;
	// Use this for initialization
	void Start () {
        sound = GetComponent<AudioSource>();
        if(sound != null)
        {
            sound.volume = GameVariables.volSound;
            sound.Play();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
