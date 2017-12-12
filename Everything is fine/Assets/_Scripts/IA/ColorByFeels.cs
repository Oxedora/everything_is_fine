using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorByFeels : MonoBehaviour {

    private Color startingColor;
    private Color fearColor;
    private Agent myAgent;
    private ParticleSystem myParticles;

	// Use this for initialization
	void Start () {
        myParticles = GetComponent<ParticleSystem>();
        startingColor = Color.green;
        myParticles.startColor = startingColor;
        fearColor = Color.blue;
        myAgent = gameObject.GetComponentInParent<Agent>();
        if(myAgent == null)
        {
            Debug.Log("Unable to get Agent in parent");
        }
	}
	
	// Update is called once per frame
	void Update () {
        myParticles.startColor = Color.Lerp(startingColor, fearColor, myAgent.Bdi.myFeelings.Fear);
    }
}
