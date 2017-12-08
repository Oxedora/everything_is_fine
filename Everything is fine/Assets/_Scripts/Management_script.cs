using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Management_script : MonoBehaviour {

	public int budget;
	public Text text_budget;
	// Use this for initialization
	void Start () {
		text_budget.text = budget + " €";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void addItem(GameObject item, int price, Vector3 pos){
		if (budget - price > 0) {
			budget -= price;
			text_budget.text = budget + " €";
			GameObject.Instantiate (item,pos,Quaternion.identity);
		}
	}

	void deleteItem(GameObject item, int price){
		GameObject.Destroy (item);
		budget += price;
		text_budget.text = budget + " €";
	}
}
