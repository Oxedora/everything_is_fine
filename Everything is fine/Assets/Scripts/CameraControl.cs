using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject camPosition;

	public float moveSpeed = 2.0f;
	public float rotateSpeed = 2.0f;

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

		//Camera movement
		moveCam ();
		rotateCam ();
	}


	void moveCam(){
		Vector3 pos = Input.mousePosition;
		Debug.Log (pos);
		float x_dir = 0;
		float z_dir = 0;
		bool changed = false;

		if (pos.y < height5) {
			z_dir -= moveSpeed * Time.deltaTime;
			Debug.Log ("Reduction du X");
			changed = true;
		} else if (pos.y > height - height5) {
			z_dir += moveSpeed * Time.deltaTime;
			Debug.Log ("Augmentation du X");
			changed = true;
		}

		if (pos.x < width5) {
			x_dir -= moveSpeed * Time.deltaTime;
			Debug.Log ("Reduction du Z");
			changed = true;
		} else if (pos.x > width - width5) {
			x_dir += moveSpeed * Time.deltaTime;
			Debug.Log ("Augmentation du Z");
			changed = true;
		}

		if (changed) {
			Vector3 new_pos = camPosition.transform.localPosition + transform.TransformDirection(new Vector3 (x_dir, 0, z_dir));
			camPosition.transform.localPosition = new_pos;
		}
	}

	void rotateCam(){
		Vector3 new_rot = camPosition.transform.rotation.eulerAngles;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			new_rot.y += rotateSpeed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			new_rot.y -= rotateSpeed * Time.deltaTime;
		}

		camPosition.transform.rotation = Quaternion.Euler(new_rot);
	}
}
