using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Management_script : MonoBehaviour {

	public int budget;
	public Text text_budget;
    public List<GameObject> objects_signalisation;
    GameObject target;
    RaycastHit hitInfo;
    // Use this for initialization
    void Start () {
		text_budget.text = budget + " €";
        objects_signalisation = new List<GameObject>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            target = ReturnClickedPos(out hitInfo);
            deleteItem(target, 10);
        }
    }

	public void addItem(GameObject item, int price, Vector3 pos, GameObject target){
		if (budget - price > 0) {
			budget -= price;
			text_budget.text = budget + " €";
            Debug.Log("forward target " + target.transform.localRotation+ " quaternion identity " + Quaternion.identity);
			GameObject clone = GameObject.Instantiate (item,pos, target.transform.rotation);
            objects_signalisation.Add(clone);

        }
	}

    public void deleteItem(GameObject item, int price){
        if (objects_signalisation.Contains(item))
        {
            objects_signalisation.Remove(item);
            GameObject.Destroy(item);
            budget += price;
            text_budget.text = budget + " €";
        }
	}


    public GameObject ReturnClickedPos(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}
