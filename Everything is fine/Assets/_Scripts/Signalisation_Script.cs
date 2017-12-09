
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signalisation_Script : MonoBehaviour
{

    public GameObject prefab;
    public GameObject button;
    GameObject p;
    Ray ray;
    RaycastHit hit;
    bool active = false;
    GameObject target;
    GameObject manage;
    GameObject sol;



    // Use this for initialization
    void Start()
    {
        manage = GameObject.Find("GameManager");
        sol = GameObject.Find("Sol");
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            RaycastHit hitInfo;
            target = manage.GetComponent<Management_script>().ReturnClickedPos(out hitInfo);
            if(target != sol)
            {
                if (Input.GetMouseButtonDown(0))
                {
					manage.GetComponent<Management_script>().addItem(prefab, 10, hitInfo.point, hitInfo.normal);
                    active = false;
                }
            }
        }
    }

    public void OnClick()
    {
        active = !active;
    }
}
