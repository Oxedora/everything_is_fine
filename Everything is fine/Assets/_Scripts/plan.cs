using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plan : MonoBehaviour {

    public GameObject prefab;
    GameObject p;
    Ray ray;
    RaycastHit hit;
    bool active = false;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        
        if (!active)
        {
            p = Instantiate(prefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z)), Quaternion.identity) as GameObject;
        }
        else
        {
            Destroy(p);
        }
        active = !active;
    }
}
