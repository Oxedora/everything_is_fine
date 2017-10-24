using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject camPosition;

	public float speed = 2.0f;

	private float width;
	private float height;

	private float width5;
	private float height5;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Confined;
		height = Screen.height;
		width = Screen.width;

		//5% de la hauteur et de la largeur de l'ecran
		width5 = (width * 5) / 100;
		height5 = (height * 5) / 100;
		//this.transform.LookAt (camPosition.transform);



	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = Input.mousePosition;
		Debug.Log (pos);
		float newX = camPosition.transform.position.x;
		float newZ = camPosition.transform.position.z;
		bool changed = false;

		if (pos.y < height5) {
			newX += speed * Time.deltaTime;
			Debug.Log ("Reduction du X");
			changed = true;
		} else if (pos.y > height - height5) {
			newX -= speed * Time.deltaTime;
			Debug.Log ("Augmentation du X");
			changed = true;
		}

		if (pos.x < width5) {
			newZ -= speed * Time.deltaTime;
			Debug.Log ("Reduction du Z");
			changed = true;
		} else if (pos.x > width - width5) {
			newZ += speed * Time.deltaTime;
			Debug.Log ("Augmentation du Z");
			changed = true;
		}

		if(changed)
			camPosition.transform.position = new Vector3 (newX,camPosition.transform.position.y,newZ);

	}
}
