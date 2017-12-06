using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour {

    bool active = false;
    public GameObject panel;

    private void Start()
    {
        //panel = GameObject.Find("Panel");
        panel.SetActive(false);
    }

    public void Act()
    {
        panel.SetActive(!active);
        active = !active;
        Debug.Log("Click");

    }
}
