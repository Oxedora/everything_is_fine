using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public GameObject camPosition;

	public float moveSpeed = 2.0f;
	public float rotateSpeed = 45.0f;

	public float maxDist = 30.0f;
	public float zoomSpeed = 10.0f;

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
		width5 = (width * 1) / 100;
		height5 = (height * 1) / 100;
		this.transform.LookAt (camPosition.transform);



	}
	
	// Update is called once per frame
	void Update () {

		//Camera movement
		moveCam ();
		rotateCam ();
		zoomCam ();
	}


	void moveCam(){
		Vector3 pos = Input.mousePosition;
		float x_dir = 0;
		float z_dir = 0;
		bool changed = false;

		if (pos.y < height5) {
			x_dir += moveSpeed * Time.deltaTime;
			changed = true;
		} else if (pos.y > height - height5) {
			x_dir -= moveSpeed * Time.deltaTime;
			changed = true;
		}

		if (pos.x < width5) {
			z_dir -= moveSpeed * Time.deltaTime;
			changed = true;
		} else if (pos.x > width - width5) {
			z_dir += moveSpeed * Time.deltaTime;
			changed = true;
		}

		if (changed) {
			Vector3 new_pos = camPosition.transform.localPosition + camPosition.transform.TransformDirection(new Vector3 (x_dir, 0, z_dir));
			camPosition.transform.localPosition = new_pos;
		}
	}

	void rotateCam(){
		Vector3 new_rot = camPosition.transform.rotation.eulerAngles;
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			new_rot.y += rotateSpeed;
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			new_rot.y -= rotateSpeed;
		}
		camPosition.transform.rotation = Quaternion.Euler(new_rot);
	}

	void zoomCam() {
		float wheel = Input.GetAxis ("Mouse ScrollWheel");
		if (wheel != 0) {
			wheel = Mathf.Sign (Input.GetAxis ("Mouse ScrollWheel"));
			if (wheel == -1 && Vector3.Distance (transform.position, camPosition.transform.position) > maxDist) {
				wheel = 0;
			} else if (wheel == 1 && Vector3.Distance (transform.position, camPosition.transform.position) < 10) {
				wheel = 0;
			}
		}
		transform.position = transform.position + transform.TransformDirection (new Vector3 (0, 0, zoomSpeed * wheel));
	}

}
